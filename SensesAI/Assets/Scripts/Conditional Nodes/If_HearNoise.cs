using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class If_HearNoise : LeafNode
{
    public If_HearNoise(Enemy agent, Player target, GameManager gameManager) : base(agent, target, gameManager)
    {

    }

    public override NodeState Evaluate()
    {
        //Hear player if they're running
        if (target.isRunning)
        {
            if (Vector3.Distance(target.transform.position, agent.transform.position) < agent.hearingRange / 2.25)
            {
                agent.memorizedPosition = target.transform.position;
                agent.memorizedObjectTimer = 0f;
                return NodeState.Success;
            }
        }

        //Hear gates opening
        foreach (Gate gate in gameManager.gates)
        {
            if (gate.rising)
            {
                if (Vector3.Distance(gate.transform.position, agent.transform.position) < agent.hearingRange)
                {
                    agent.memorizedPosition = gate.transform.position;
                    agent.memorizedObjectTimer = 0f;
                    return NodeState.Success;
                }
            }
        }

        return NodeState.Failure;
    }
}
