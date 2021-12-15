using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Stores lists of all important objects in the scene
    [SerializeField] public List<Terminal> terminals;
    [SerializeField] public List<Gate> gates;
    [SerializeField] public List<Enemy> enemies;
    [SerializeField] public Player player;
    [SerializeField] public EndElevator endElevator;

    [SerializeField] public TMP_Text endText;
    [SerializeField] public TMP_Text codesCount;
    [SerializeField] public TMP_Text enemySight;
    [SerializeField] public TMP_Text enemyHearing;
    [SerializeField] public TMP_Text terminalInteractPrompt;
    [SerializeField] public TMP_Text gateInteractPrompt;

    // Start is called before the first frame update
    void Start()
    {
        SetCodesUI(0);
        enemySight.text = "ACTIVE";
        enemyHearing.text = "Disabled";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Determine if any terminals are within interaction range of the player
    public Terminal PlayerInteractableTerminal()
    {
        foreach (Terminal terminal in terminals)
        {
            if (Vector3.Distance(player.transform.position, terminal.transform.position) < player.interactDistance && terminal.hasCode)
            {
                terminalInteractPrompt.enabled = true;
                return terminal;
            }
        }

        terminalInteractPrompt.enabled = false;
        return null;
    }

    //Determine if any gates are within interaction range of the player
    public Gate PlayerInteractableGate()
    {
        foreach (Gate gate in gates)
        {
            if (Vector3.Distance(player.transform.position, gate.transform.position) < player.interactDistance && !gate.rising && !gate.raised)
            {
                gateInteractPrompt.enabled = true;
                return gate;
            }
        }

        gateInteractPrompt.enabled = false;
        return null;
    }

    //Determine if any gates are within interaction range of the player
    public bool PlayerInteractableElevator()
    {
        if (Vector3.Distance(player.transform.position, endElevator.transform.position) < player.interactDistance)
        {
            endText.enabled = true;
            return true;
        }

        return false;
    }

    //Player's code count goes up by 1
    public void SetCodesUI(int codesFound)
    {
        codesCount.text = codesFound + "/" + terminals.Count;
    }

    //Check if the game should end based on how many codes the player has found
    public void SetEndUI(int codesFound)
    {
        //If the player has found every code, "end the game" by stopping time.
        if (codesFound >= terminals.Count)
        {
            endText.text = "Codes received.\nMission Complete!";
            Time.timeScale = 0;
        }
        //Otherwise, tell the player they don't have all the codes
        else
        {
            endText.text = "Missing Codes!";
        }
    }

    //End the game because the player has been caught
    public void SetEndUI()
    {
        endText.enabled = true;
        endText.text = "Caught by Robots.\nMission Failed.";
        Time.timeScale = 0;
    }

    //Disable sight sense of all enemies
    public void DisableSight()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.canSee = false;
            enemy.canHear = true;
            enemySight.text = "Disabled";
            enemyHearing.text = "ACTIVE";
        }
    }

    //Disable hearing sense for all enemies
    public void DisableHearing()
    {
        foreach(Enemy enemy in enemies)
        {
            enemy.canHear = false;
            enemy.canSee = true;
            enemySight.text = "ACTIVE";
            enemyHearing.text = "Disabled";
        }
    }
}
