using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class If_CanHear : LeafNode
{
    public If_CanHear(Enemy agent, Player target, GameManager gameManager) : base(agent, target, gameManager)
    {

    }

    public override NodeState Evaluate()
    {
        if (agent.canHear)
        {
            return NodeState.Success;
        }

        return NodeState.Failure;
    }
}
