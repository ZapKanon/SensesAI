using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class If_KnowLastPosition : LeafNode
{
    public If_KnowLastPosition(Enemy agent, Player target, GameManager gameManager) : base(agent, target, gameManager)
    {

    }

    public override NodeState Evaluate()
    {

        if (agent.memorizedPosition != new Vector3())
        {
            return NodeState.Success;
        }

        return NodeState.Failure;
    }
}
