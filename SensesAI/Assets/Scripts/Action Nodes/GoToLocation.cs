using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToLocation : LeafNode
{
    private Vector3 destination;

    public GoToLocation(Enemy agent, Player target, GameManager gameManager) : base(agent, target, gameManager)
    {

    }

    public override NodeState Evaluate()
    {
        agent.GetComponent<NavMeshAgent>().SetDestination(agent.memorizedPosition);
        return NodeState.Success;
    }
}
