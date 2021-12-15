using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The basis for all node types within the behavior tree.
public enum NodeState
{ 
    Success,
    Failure,
    Running
}

public abstract class Node : MonoBehaviour
{
    public NodeState currentState;

    public Node() 
    { 
    
    }

    public abstract NodeState Evaluate();
}
