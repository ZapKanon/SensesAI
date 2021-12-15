using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A node which checks each of its children in order, returning success if any of them succeed.
public class SelectorNode : Node
{
    //List of this selector node's children
    protected List<Node> childNodes = new List<Node>();

    public SelectorNode(List<Node> childNodes)
    {
        this.childNodes = childNodes;
    }

    public override NodeState Evaluate()
    {
        foreach (Node childNode in childNodes)
        {
            switch (childNode.Evaluate())
            {
                case NodeState.Failure:
                    continue;

                case NodeState.Success:
                    currentState = NodeState.Success;
                    return currentState;

                case NodeState.Running:
                    currentState = NodeState.Running;
                    return currentState;

                default:
                    continue;
            }
        }

        //If all child nodes fail, return failure for this selector
        currentState = NodeState.Failure;
        return currentState;
    }
}
