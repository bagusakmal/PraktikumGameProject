using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 move;
    public float forwardSpeed;

    private int desiredLane = 1; // 0 left, 1 middle, 2 right
    [SerializeField]
    private float laneDistance = 3f; // the distance between two lanes

    private float gravity = -12f;
    public float jumpForce = 2;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.isGameStarted)
            return;

        move.z = forwardSpeed;

        if (controller.isGrounded)
        {
            move.y = -1;

            if (Input.GetKeyDown(KeyCode.UpArrow) || SweepManager.swipeUp)
            {
                Jump();
            }
        }
        else
        {
            move.y += gravity * Time.deltaTime;
        }

        controller.Move(move * Time.deltaTime);

        // Gather the input which lane we should be 
        if (Input.GetKeyDown(KeyCode.RightArrow) || SweepManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || SweepManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        // calculate where should be in future
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
            targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * laneDistance;

        transform.position = targetPosition;
    }

    private void FixedUpdate()
    {
        if (!PlayerManager.isGameStarted)
            return;

        controller.Move(move * Time.fixedDeltaTime);

    }

    private void Jump()
    {
        move.y = jumpForce;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Obstacle"))
        {
            PlayerManager.gameOver = true;
            // FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
    }

    


}