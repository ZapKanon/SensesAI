using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Enemies are robots that patrol the facility.
//They use sight and hearing to patrol and search for the player.
public class Enemy : MonoBehaviour
{
    public bool canSee;
    public bool canHear;
    public float nearDistance;
    public float atDistance;
    public Player player;
    public GameManager gameManager;

    private Node rootNode;

    public Vector3 memorizedPosition;
    public float memorizedObjectTimer = 0;
    public float memorizedObjectLimit;

    public float hearingRange;
    public List<Transform> patrolPoints;
    public int currentPatrolPoint;
    public bool patrolling;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SetUpBehaviorTree();

        canSee = true;
        canHear = false;
        patrolling = false;

        nearDistance = 12.0f;
        atDistance = 6.0f;
        //GetComponent<NavMeshAgent>().speed = 40.0f;
        memorizedObjectLimit = 10.0f;
        hearingRange = 120.0f;
        currentPatrolPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        rootNode.Evaluate();
        Debug.Log(Vector3.Distance(player.transform.position, transform.position));

        //Forget about an object's location after some amount of time
        if (memorizedPosition != new Vector3())
        {
            memorizedObjectTimer += Time.deltaTime;
            
            if (memorizedObjectTimer >= memorizedObjectLimit)
            {
                memorizedPosition = new Vector3();
            }
        }

        //Enemies move more slowly while patrolling
        if (patrolling)
        {
            GetComponent<NavMeshAgent>().speed = 15.0f;
            patrolling = false;
        }
        else
        {
            GetComponent<NavMeshAgent>().speed = 34.0f;
        }
    }

    //Create and organize all nodes present in this enemy's behavior tree
    private void SetUpBehaviorTree()
    {
        //The tree's nodes need to be created starting from leaves and working backwards, so they can be added to parent nodes' lists

        //Kill the player if close enough. Not reliant on senses
        If_AtPlayer if_AtPlayer = new If_AtPlayer(this, player, gameManager);
        KillPlayer killPlayer = new KillPlayer(this, player, gameManager);
        SequenceNode killPlayerSequence = new SequenceNode(new List<Node>() { if_AtPlayer, killPlayer });

        //Chase the player if close enough but not close enough to kill them. Not reliant on senses
        If_NearPlayer if_NearPlayer = new If_NearPlayer(this, player, gameManager);
        GoToLocation goToPlayer = new GoToLocation(this, player, gameManager);
        SequenceNode chasePlayerSequence = new SequenceNode(new List<Node>() { if_NearPlayer, goToPlayer });

        //See an object and move toward it
        If_SeeObject if_SeeObject = new If_SeeObject(this, player, gameManager);
        GoToLocation goToSeeingObject = new GoToLocation(this, player, gameManager);
        SequenceNode seeingObjectSequence = new SequenceNode(new List<Node>() { if_SeeObject, goToSeeingObject });

        //Move toward an last-seen location of object
        If_KnowLastPosition if_SeenLastPosition = new If_KnowLastPosition(this, player, gameManager);
        GoToLocation goToSeenObject = new GoToLocation(this, player, gameManager);
        SequenceNode seenObjectSequence = new SequenceNode(new List<Node>() { if_SeenLastPosition, goToSeenObject });

        //Wait, can we even see? (the tree is being built backwards, remember. This is a joke.)
        If_CanSee if_CanSee = new If_CanSee(this, player, gameManager);
        SelectorNode sightSelector = new SelectorNode(new List<Node>() { seeingObjectSequence, seenObjectSequence });
        SequenceNode sightCheckSequence = new SequenceNode(new List<Node>() { if_CanSee, sightSelector });

        //Hear a noise and move toward it
        If_HearNoise if_HearNoise = new If_HearNoise(this, player, gameManager);
        GoToLocation goToHearingObject = new GoToLocation(this, player, gameManager);
        SequenceNode hearingObjectSequence = new SequenceNode(new List<Node>() { if_HearNoise, goToHearingObject });

        //Move toward location of previously-heard noise
        If_KnowLastPosition if_HeardLastPosition = new If_KnowLastPosition(this, player, gameManager);
        GoToLocation goToHeardObject = new GoToLocation(this, player, gameManager);
        SequenceNode heardObjectSequence = new SequenceNode(new List<Node>() { if_HeardLastPosition, goToHeardObject });

        //Wait, can we even hear? (the tree is being built backwards, remember. This is the same joke.)
        If_CanHear if_CanHear = new If_CanHear(this, player, gameManager);
        SelectorNode hearingSelector = new SelectorNode(new List<Node>() { hearingObjectSequence, heardObjectSequence });
        SequenceNode hearingCheckSequence = new SequenceNode(new List<Node>() { if_CanHear, hearingSelector });

        //Sense-related selector node
        SelectorNode sensesSelector = new SelectorNode(new List<Node>() { sightCheckSequence, hearingCheckSequence });

        //Patrol power!
        Patrol patrol = new Patrol(this, player, gameManager);

        //Finally, the root node that governs the entire tree
        rootNode = new SelectorNode(new List<Node>() { killPlayerSequence, chasePlayerSequence, sensesSelector, patrol});
    }

    public void UpdatePatrolPoint()
    {
        if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) <= GetComponent<NavMeshAgent>().stoppingDistance)
        {
            currentPatrolPoint++;

            if (currentPatrolPoint >= patrolPoints.Count)
            {
                currentPatrolPoint = 0;
            }
        }
    }
}
