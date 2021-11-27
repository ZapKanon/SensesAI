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

    private GameManager gameManager;
    public int codesFound;

    // Start is called before the first frame update
    void Start()
    {
        walkSpeed = 15.0f;
        runSpeed = 35.0f;
        rotateSpeed = 180.0f;
        interactDistance = 10.0f;
        codesFound = 0;
        playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.freezeRotation = true;
        playerModel = gameObject.transform.Find("PlayerModel").gameObject;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            positionChange += Vector3.forward;
            rotationChange = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            positionChange += Vector3.left;
            rotationChange = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            positionChange += Vector3.back;
            rotationChange = true;
        }

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
        else if (nearbyGate != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (nearbyGate.Raise())
                {

                }
            }
        }
    }
}
