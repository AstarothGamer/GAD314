using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 15f;
    [SerializeField] private float jumpForce = 6f;

    [Header("Look Settings")]
    [SerializeField] private Camera playerCam;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float maxLookAngle = 85f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.3f;
    [SerializeField] private LayerMask groundMask;

    private Rigidbody rb;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private float rotationY;
    private float rotationX;
    public bool isGrounded;
    private bool isRunning;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;

        currentSpeed = walkSpeed;
    }

    void Update()
    {
        Look();
        HandleInput();
        Run();
        Jump();
    }

    void FixedUpdate()
    {
        GroundCheck();
        Movement();
    }

    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -maxLookAngle, maxLookAngle);

        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
        playerCam.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }

    private void HandleInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        moveInput = (transform.forward * z + transform.right * x).normalized;
    }

    private void Movement()
    {
        float targetSpeed = isRunning ? runSpeed : walkSpeed;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, 5f * Time.fixedDeltaTime);

        Vector3 targetVelocity = moveInput * currentSpeed;

        if (moveInput.magnitude > 0.1f)
            moveVelocity = Vector3.Lerp(moveVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        else
            moveVelocity = Vector3.Lerp(moveVelocity, Vector3.zero, deceleration * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector3(moveVelocity.x, rb.linearVelocity.y, moveVelocity.z);
    }

    private void Run()
    {
        isRunning = Input.GetKey(KeyCode.LeftShift);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); 
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }
}