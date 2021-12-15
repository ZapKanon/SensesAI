using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class If_SeeObject : LeafNode
{
    public If_SeeObject(Enemy agent, Player target, GameManager gameManager) : base(agent, target, gameManager)
    {

    }

    public override NodeState Evaluate()
    {
        //TODO: View cone see player stuff

        foreach(Terminal terminal in gameManager.terminals)
        {
            if (terminal.lit)
            {
                if (terminal.sightZone.bounds.Contains(agent.transform.position))
                {
                    agent.memorizedPosition = terminal.transform.position;
                    agent.memorizedObjectTimer = 0f;
                    return NodeState.Success;
                }
            }            
        }

        return NodeState.Failure;
    }
}
