using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class If_CanSee : LeafNode
{
    public If_CanSee(Enemy agent, Player target, GameManager gameManager) : base(agent, target, gameManager)
    {

    }

    public override NodeState Evaluate()
    {
        if (agent.canSee)
        {
            return NodeState.Success;
        }

        return NodeState.Failure;
    }
}
