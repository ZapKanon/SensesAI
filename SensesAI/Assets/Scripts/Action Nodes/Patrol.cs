using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : LeafNode
{
    // Start is called before the first frame update
    public Patrol(Enemy agent, Player target, GameManager gameManager) : base(agent, target, gameManager)
    {

    }

    //Run the gameManager method that ends the game with the player being caught
    public override NodeState Evaluate()
    {
        agent.UpdatePatrolPoint();
        agent.GetComponent<NavMeshAgent>().SetDestination(agent.patrolPoints[agent.currentPatrolPoint].position);
        agent.patrolling = true;
        return NodeState.Success;
    }
}
