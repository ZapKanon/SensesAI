using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : LeafNode
{
    public KillPlayer(Enemy agent, Player target, GameManager gameManager) : base(agent, target, gameManager)
    {

    }

    //Run the gameManager method that ends the game with the player being caught
    public override NodeState Evaluate()
    {
        gameManager.SetEndUI();
        return NodeState.Success;
    }
}
