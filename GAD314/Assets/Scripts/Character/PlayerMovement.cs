using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Reference to the player's camera and orientation objects
    public Transform playerCam;
    public Transform orientation;
    private Rigidbody rb;

    // Rotation and look variables
    private float xRotation;
    private float sensitivity = 50f;
    private float sensMultiplier = 1f;

    // Movement variables
    public float moveSpeed = 4500;
    public float maxSpeed = 20;
    public bool grounded;
    public LayerMask whatIsGround;

    // Variables to counteract sliding and sloppy movement
    public float counterMovement = 0.175f;
    private float threshold = 0.01f;
    public float maxSlopeAngle = 35f;

    // Crouch & Slide variables
    private Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    private Vector3 playerScale;
    public float slideForce = 400;
    public float slideCounterMovement = 0.2f;

    // Jumping variables
    private bool readyToJump = true;
    private float jumpCooldown = 0.25f;
    public float jumpForce = 550f;

    // Input tracking variables
    float x, y;
    bool jumping, sprinting, crouching;

    // Sliding-related variables
    private Vector3 normalVector = Vector3.up;
    private Vector3 wallNormalVector;

    // public bool used = false;

    void Awake()
    {
        // Initialize the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Store the player's original scale and lock the cursor
        playerScale = transform.localScale;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        // Handle player movement in FixedUpdate for physics
        Movement();
    }

    private void Update()
    {
        // Handle user input and look controls in Update (frame update)
        MyInput();
        Look();
    }

    private void MyInput()
    {
        // Track input values for movement and actions (jumping, crouching)
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        jumping = Input.GetButton("Jump");
        crouching = Input.GetKey(KeyCode.C);

        // Crouch behavior
        if (Input.GetKeyDown(KeyCode.C))
            StartCrouch();
        if (Input.GetKeyUp(KeyCode.C))
            StopCrouch();
    }

    private void StartCrouch()
    {
        // Adjust player scale for crouching and apply slide force if moving
        transform.localScale = crouchScale;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        if (rb.linearVelocity.magnitude > 0.5f && grounded)
        {
            rb.AddForce(orientation.transform.forward * slideForce); // Apply slide force
        }
    }

    private void StopCrouch()
    {
        // Restore the player's original scale when uncrouching
        transform.localScale = playerScale;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    private void Movement()
    {
        // Apply extra gravity to the player
        rb.AddForce(Vector3.down * Time.deltaTime * 10);

        // Get the actual velocity relative to where the player is looking
        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;

        // Counteract sliding and sloppy movement
        CounterMovement(x, y, mag);

        // Handle jumping if the player is ready and the jump button is pressed
        if (readyToJump && jumping) Jump();

        // Ensure the player doesn't exceed max speed
        float maxSpeed = this.maxSpeed;

        // If sliding down a ramp, add downward force to keep the player grounded
        if (crouching && grounded && readyToJump)
        {
            rb.AddForce(Vector3.down * Time.deltaTime * 3000);
            return;
        }

        // Cancel out movement if max speed is exceeded
        if (x > 0 && xMag > maxSpeed) x = 0;
        if (x < 0 && xMag < -maxSpeed) x = 0;
        if (y > 0 && yMag > maxSpeed) y = 0;
        if (y < 0 && yMag < -maxSpeed) y = 0;

        // Movement multipliers based on the player's state (in air, sliding, etc.)
        float multiplier = 1f, multiplierV = 1f;
        if (!grounded) { multiplier = 0.5f; multiplierV = 0.5f; }
        if (grounded && crouching) multiplierV = 0f;

        // Apply forces to move the player
        rb.AddForce(orientation.transform.forward * y * moveSpeed * Time.deltaTime * multiplier * multiplierV);
        rb.AddForce(orientation.transform.right * x * moveSpeed * Time.deltaTime * multiplier);
    }

    private void Jump()
    {
        // Perform jump if the player is grounded and ready to jump
        if (grounded && readyToJump)
        {
            readyToJump = false;

            // Apply upward and forward jump forces
            rb.AddForce(Vector2.up * jumpForce * 1.5f);
            rb.AddForce(normalVector * jumpForce * 0.5f);

            // Adjust vertical velocity during a jump
            Vector3 vel = rb.linearVelocity;
            if (rb.linearVelocity.y < 0.5f)
                rb.linearVelocity = new Vector3(vel.x, 0, vel.z);
            else if (rb.linearVelocity.y > 0)
                rb.linearVelocity = new Vector3(vel.x, vel.y / 2, vel.z);

            // Reset jump state after a cooldown
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ResetJump()
    {
        // Allow jumping again after the cooldown
        readyToJump = true;
    }

    private float desiredX;
    private void Look()
    {
        // Track mouse movement for looking around
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;

        // Update the camera rotation based on the mouse input
        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        // Clamp the vertical rotation to avoid flipping
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply the rotations to the camera and orientation
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }

    private void CounterMovement(float x, float y, Vector2 mag)
    {
        // Skip if not grounded or jumping
        if (!grounded || jumping) return;

        // Apply counter forces to prevent sliding during movement
        if (crouching)
        {
            rb.AddForce(moveSpeed * Time.deltaTime * -rb.linearVelocity.normalized * slideCounterMovement);
            return;
        }

        // Apply forces to counteract movement when no input is given
        if (Math.Abs(mag.x) > threshold && Math.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.right * Time.deltaTime * -mag.x * counterMovement);
        }
        if (Math.Abs(mag.y) > threshold && Math.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.forward * Time.deltaTime * -mag.y * counterMovement);
        }

        // Limit the player's speed to the max speed
        if (Mathf.Sqrt((Mathf.Pow(rb.linearVelocity.x, 2) + Mathf.Pow(rb.linearVelocity.z, 2))) > maxSpeed)
        {
            float fallspeed = rb.linearVelocity.y;
            Vector3 n = rb.linearVelocity.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(n.x, fallspeed, n.z);
        }
    }

    // Find the velocity relative to the player's look direction
    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = orientation.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.linearVelocity.x, rb.linearVelocity.z) * Mathf.Rad2Deg;

        // Calculate the relative velocity components
        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitude = rb.linearVelocity.magnitude;
        float yMag = magnitude * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitude * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }

    // Check if a given vector represents a walkable surface (floor)
    private bool IsFloor(Vector3 v)
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    private bool cancellingGrounded;

    // Detect collisions with the ground to determine if the player is grounded
    private void OnCollisionStay(Collision other)
    {
        int layer = other.gameObject.layer;
        if (whatIsGround != (whatIsGround | (1 << layer))) return;

        // Check each contact point in the collision
        for (int i = 0; i < other.contactCount; i++)
        {
            Vector3 normal = other.contacts[i].normal;
            if (IsFloor(normal))
            {
                grounded = true;
                cancellingGrounded = false;
                normalVector = normal;
                CancelInvoke(nameof(StopGrounded));
            }
        }

        // Delay grounded status reset
        float delay = 3f;
        if (!cancellingGrounded)
        {
            cancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * delay);
        }
    }

    private void StopGrounded()
    {
        grounded = false;
    }
}
