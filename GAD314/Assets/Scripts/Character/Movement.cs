using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection = Vector3.zero;
    [SerializeField] private float currentSpeed = 4;

    private float y = 0.0f;
    private float x = 0.0f;
    public float maxLookAngle = 50f;
    public Camera playerCam;

    public float rotationSpeed = 10f; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    void Rotate()
    {
        y = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * rotationSpeed;

        x -= rotationSpeed * Input.GetAxis("Mouse Y");
            

        x = Mathf.Clamp(x, -maxLookAngle, maxLookAngle);

        transform.localEulerAngles = new Vector3(0, y, 0);
        playerCam.transform.localEulerAngles = new Vector3(x, 0, 0);
    }
    
    void Move()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");


        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.Normalize();
        right.Normalize();


        moveDirection = forward * vertical + right * horizontal;
        moveDirection.Normalize();

        Vector3 movement = moveDirection * currentSpeed * Time.deltaTime;
        transform.position += movement;
    }
}