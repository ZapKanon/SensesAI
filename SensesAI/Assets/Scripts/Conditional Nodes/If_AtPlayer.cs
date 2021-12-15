using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class If_AtPlayer : LeafNode
{
    public If_AtPlayer(Enemy agent, Player target, GameManager gameManager) : base(agent, target, gameManager)
    {

    }

    public override NodeState Evaluate()
    {
        //If the player is within the enemy's atDistance, return success (not reliant on senses)
        if (Vector3.Distance(agent.transform.position, target.transform.position) <= agent.atDistance)
        {
            agent.memorizedPosition = target.transform.position;
            return NodeState.Success;
        }

        return NodeState.Failure;
    }
}
