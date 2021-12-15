using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gates block the player's progress.
//The player can interact with a gate to open it, but this creates a lot of noise that may attract enemies.
public class Gate : MonoBehaviour
{
    //Has this gate been raised, allowing the player to pass through?
    public bool raised = false;

    //Is the gate currently rising?
    public bool rising = false;

    //Location at which the gate is fully raised
    Vector3 raisedPosition;

    float raiseSpeed = 0.65f;
    
    //The part of the gate that actually rises
    GameObject actualGate;
    AudioSource gateAudio;

    // Start is called before the first frame update
    void Start()
    {
        actualGate = gameObject.transform.Find("ActualGate").gameObject;
        raisedPosition = new Vector3(actualGate.transform.position.x, actualGate.transform.position.y + 6, actualGate.transform.position.z);
        gateAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Raise gate over time
        if (!raised && rising && actualGate.transform.position != raisedPosition)
        {
            actualGate.transform.position += new Vector3(0, raiseSpeed * Time.deltaTime, 0);

            if (actualGate.transform.position.y >= raisedPosition.y)
            {
                actualGate.transform.position = raisedPosition;
                raised = true;
                rising = false;
            }
        }
    }

    //Called when the player interacts with the gate
    //Raise the actual gate so the player can pass through
    public bool Raise()
    {
        if (!raised && !rising)
        {
            rising = true;
            gateAudio.Play();
            return true;
        }

        return false;
    }
}
