using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class If_NearPlayer : LeafNode
{

    public If_NearPlayer(Enemy agent, Player target, GameManager gameManager) : base(agent, target, gameManager)
    {

    }

    public override NodeState Evaluate()
    {
        //If the player is within the enemy's nearDistance, return success (not reliant on senses)
        if (Vector3.Distance(agent.transform.position, target.transform.position) <= agent.nearDistance)
        {
            agent.memorizedPosition = target.transform.position;
            return NodeState.Success;
        }

        return NodeState.Failure;
    }
}
