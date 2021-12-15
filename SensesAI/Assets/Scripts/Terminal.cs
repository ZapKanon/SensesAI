using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Computer terminals contain the codes that the player needs to find.
//The player can interact with a terminal to obtain its code, but this creates a bright light that may attract enemies.
public class Terminal : MonoBehaviour
{
    //Does this terminal have a code that the player hasn't taken yet?
    public bool hasCode = true;

    //Is the terminal lit up?
    public bool lit = false;

    //Lights that turn on when the terminal is accessed
    GameObject pointLight;
    GameObject spotLight;

    //The area within which enemies can see the light of the terminal
    BoxCollider sightZone;

    //How long the lights stay on after the terminal is accessed
    private float lightTimeMax = 10.0f;
    private float lightTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        pointLight = gameObject.transform.Find("TerminalPointLight").gameObject;
        spotLight = gameObject.transform.Find("TerminalSpotLight").gameObject;
        sightZone = gameObject.transform.Find("SightZone").gameObject.GetComponent<BoxCollider>();
        pointLight.GetComponent<Light>().enabled = false;
        spotLight.GetComponent<Light>().enabled = false;
    }

    //Update light timer if lights are on
    //Shut lights off if enough time has passed 
    void Update()
    {
        if (lit)
        {
            lightTimer += Time.deltaTime;

            if (lightTimer >= lightTimeMax)
            {
                EndLight();
            }
        }
    }

    //Called when the player accesses the terminal
    //Remove the terminal's code and cast a bright light out of the terminal's screen
    public bool Access()
    {
        if (hasCode)
        {
            hasCode = false;
            CastLight();
            return true;
        }

        return false;
    }

    //Shine point light and spot light for a period of time
    private void CastLight()
    {
        if (!lit)
        {
            lit = true;
            pointLight.GetComponent<Light>().enabled = true;
            spotLight.GetComponent<Light>().enabled = true;
        }
    }

    //Turn the lights off
    private void EndLight()
    {
        if(lit)
        {
            lit = false;
            pointLight.GetComponent<Light>().enabled = false;
            spotLight.GetComponent<Light>().enabled = false;
        }
    }
}
