using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LeafNode : Node
{
    protected Enemy agent;
    protected Player target;
    protected GameManager gameManager;

    public LeafNode(Enemy agent, Player target, GameManager gameManager)
    {
        this.agent = agent;
        this.target = target;
        this.gameManager = gameManager;
    }
}
