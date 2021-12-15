using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float rotateSpeed;
    public float interactDistance;
    private Rigidbody playerRigidbody;
    private GameObject playerModel;
    private AudioSource playerAudio;

    private GameManager gameManager;
    public int codesFound;
    public bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
        walkSpeed = 15.0f;
        runSpeed = 35.0f;
        rotateSpeed = 180.0f;
        interactDistance = 10.0f;
        codesFound = 0;
        isRunning = false;
        playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.freezeRotation = true;
        playerModel = gameObject.transform.Find("PlayerModel").gameObject;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Check to move player every frame
    void Update()
    {
        PlayerMovement();
        PlayerInteract();
    }

    /// <summary>
    /// The player can move in 8 directions using WASD input
    /// The player's model rotates in the direction of movement
    /// </summary>
    private void PlayerMovement()
    {
        Vector3 positionChange = new Vector3(0.0f, 0.0f, 0.0f);
        bool rotationChange = false;
        float moveSpeed;
        
        //Run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
            isRunning = true;

            if (!playerAudio.isPlaying)
            {
                playerAudio.Play();
            }
        }
        else
        {
            moveSpeed = walkSpeed;
            isRunning = false;

            if (playerAudio.isPlaying)
            {
                playerAudio.Stop();
            }
        }

        //Move forward
        if (Input.GetKey(KeyCode.W))
        {
            positionChange += Vector3.forward;
            rotationChange = true;
        }

        //Move left
        if (Input.GetKey(KeyCode.A))
        {
            positionChange += Vector3.left;
            rotationChange = true;
        }

        //Move backward
        if (Input.GetKey(KeyCode.S))
        {
            positionChange += Vector3.back;
            rotationChange = true;
        }

        //Move right
        if (Input.GetKey(KeyCode.D))
        {
            positionChange += Vector3.right;
            rotationChange = true;
        }

        playerRigidbody.velocity = (moveSpeed * positionChange.normalized);

        if (rotationChange)
        {
            playerModel.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(positionChange.x, positionChange.z), transform.up);
        }
    }

    private void PlayerInteract()
    {
        Terminal nearbyTerminal = gameManager.PlayerInteractableTerminal();
        Gate nearbyGate = gameManager.PlayerInteractableGate();

        //If near a terminal
        if (nearbyTerminal != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (nearbyTerminal.Access())
                {
                    codesFound++;
                    gameManager.SetCodesUI(codesFound);
                }
            }
        }
        //If near a gate
        else if (nearbyGate != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (nearbyGate.Raise())
                {

                }
            }
        }

        //If near the end elevator
        if (gameManager.PlayerInteractableElevator())
        {
            gameManager.SetEndUI(codesFound);
        }

        //Disable enemy vision
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameManager.DisableSight();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            gameManager.DisableHearing();
        }
    }
}
