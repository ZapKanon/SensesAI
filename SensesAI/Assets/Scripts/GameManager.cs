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

    [SerializeField] public TMP_Text codesCount;
    [SerializeField] public TMP_Text enemySight;
    [SerializeField] public TMP_Text enemyHearing;
    [SerializeField] public TMP_Text terminalInteractPrompt;
    [SerializeField] public TMP_Text gateInteractPrompt;

    // Start is called before the first frame update
    void Start()
    {
        SetCodesUI(0);
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

    //Determine if any terminals are within interaction range of the player
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

    //Player's code count goes up by 1
    public void SetCodesUI(int codesFound)
    {
        codesCount.text = codesFound + "/" + terminals.Count;
    }
}
