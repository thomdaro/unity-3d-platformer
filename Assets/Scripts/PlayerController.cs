using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // determines our walking speed and the effective height of our jump
    public float walkSpeed = 8f;
    public float jumpSpeed = 7f;

    // physics objects, audio and HUD manager
    Rigidbody rb;
    Collider coll;
    public AudioSource coinAudioSource;
    public HUDManager hud;

    // checks whether or not we've already pressed jump during a jump
    bool pressedJump = false;

    void Start()
    {
        // assigns references to our physics objects and initializes the HUD
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        hud.Refresh();
    }

    void Update()
    {
        WalkHandler();
        JumpHandler();
    }

    void WalkHandler()
    {
        // pulls the existing vertical velocity to avoid interrupting a jump
        rb.velocity = new Vector3(0, rb.velocity.y, 0);

        // gets the distance to move on the next frame and our axis inputs
        float distance = walkSpeed * Time.deltaTime;
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        // uses the vertical input to move left/right and flipped horizontal input for back/forward
        Vector3 movement = new Vector3(vAxis * distance, 0f, hAxis * distance * -1);
        Vector3 currPosition = transform.position;
        Vector3 newPosition = currPosition + movement;

        // moves our physics object to the new position
        rb.MovePosition(newPosition);
    }

    void JumpHandler()
    {
        // checks if our jump axis is receiving input and if we're on the ground
        float jAxis = Input.GetAxis("Jump");
        bool isGrounded = CheckGrounded();

        // our jump axis is receiving input
        if (jAxis > 0f)
        {
            // and we haven't already pressed jump and we're on the ground
            if (!pressedJump && isGrounded)
            {
                // press jump and give our player some vertical speed
                pressedJump = true;
                Vector3 jumpVector = new Vector3(0f, jumpSpeed, 0f);
                rb.velocity += jumpVector;
            }
        }
        else
        {
            // if we aren't receiving input, mark the flag as such
            pressedJump = false;
        }
    }

    bool CheckGrounded()
    {
        // get the boundaries of our collision box
        float sizeX = coll.bounds.size.x;
        float sizeZ = coll.bounds.size.z;
        float sizeY = coll.bounds.size.y;

        // the "corners" are slightly below the actual model to avoid the raycast colliding with the model itself
        Vector3 corner1 = transform.position + new Vector3(sizeX / 2, -sizeY / 2 + 0.01f, sizeZ / 2);
        Vector3 corner2 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + 0.01f, sizeZ / 2);
        Vector3 corner3 = transform.position + new Vector3(sizeX / 2, -sizeY / 2 + 0.01f, -sizeZ / 2);
        Vector3 corner4 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + 0.01f, -sizeZ / 2);

        // raycast starts at the corners, looks downward, and if a model is within 0.01 units, it considers the player grounded
        bool grounded1 = Physics.Raycast(corner1, new Vector3(0, -1, 0), 0.01f);
        bool grounded2 = Physics.Raycast(corner2, new Vector3(0, -1, 0), 0.01f);
        bool grounded3 = Physics.Raycast(corner3, new Vector3(0, -1, 0), 0.01f);
        bool grounded4 = Physics.Raycast(corner4, new Vector3(0, -1, 0), 0.01f);

        // if any of the corners is grounded, the player is grounded
        return (grounded1 || grounded2 || grounded3 || grounded4);
    }

    void OnTriggerEnter(Collider collider)
    {
        // if we're touching a coin
        if (collider.gameObject.CompareTag("Coin"))
        {
            // add 1 to our score
            GameManager.instance.IncreaseScore(1);
            // refresh the HUD
            hud.Refresh();
            // play the coin collect sound
            coinAudioSource.Play();
            // remove the coin from the game
            Destroy(collider.gameObject);
        }
        // if we touch an enemy
        else if (collider.gameObject.CompareTag("Enemy"))
        {
            // go to the game over screen
            SceneManager.LoadScene("Game Over");
        }
        // if we touch the goal
        else if (collider.gameObject.CompareTag("Goal"))
        {
            // go to the next level (or loop around)
            GameManager.instance.IncreaseLevel();
        }
    }
}
