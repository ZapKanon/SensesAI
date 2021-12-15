using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A node which avaluates its children in order, returning success if all children have succeeded.
public class SequenceNode : Node
{
    //List of this selquence node's children
    protected List<Node> childNodes = new List<Node>();

    public override NodeState Evaluate()
    {
        bool childNodeRunning = false;

        foreach (Node childNode in childNodes)
        {
            switch (childNode.Evaluate())
            {
                case NodeState.Failure:
                    currentState = NodeState.Failure;
                    return currentState;

                case NodeState.Success:
                    continue;

                case NodeState.Running:
                    childNodeRunning = true;
                    continue;

                default:
                    currentState = NodeState.Success;
                    return currentState;
            }
        }

        //Return running if any child is running
        if (childNodeRunning)
        {
            currentState = NodeState.Running;
        }
        //If all children have returned success, this node returns success
        else
        {
            currentState = NodeState.Success;
        }

        return currentState;
    }
}
