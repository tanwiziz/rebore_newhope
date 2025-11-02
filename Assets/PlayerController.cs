using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5.0f; // ความเร็วในการเดินปกติ
    public float sprintSpeed = 10.0f; // ความเร็วเมื่อกด Shift
    public float jumpHeight = 2.0f; 
    public float gravity = -9.81f * 2; 

    [Header("Camera Rotation Settings")]
    public float mouseSensitivity = 3.0f;
    public float lookUpLimit = 90.0f;
    public float lookDownLimit = -90.0f;

    private CharacterController controller;
    private Vector3 velocity;
    private float rotationX = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // === 1. การจัดการความเร็ว (Sprint Logic) ===
        
        // ตรวจสอบว่าผู้เล่นกำลังกดปุ่ม Shift ซ้ายหรือไม่
        // Input.GetKey(KeyCode.LeftShift) จะคืนค่า true หากปุ่มกำลังถูกกด
        float currentSpeed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed; // เปลี่ยนความเร็วเป็นความเร็ววิ่ง
        }


        // === 2. การเคลื่อนที่และการกระโดด ===
        
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        float horizontalInput = Input.GetAxis("Horizontal"); 
        float verticalInput = Input.GetAxis("Vertical"); 
        
        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;

        // *** ใช้ currentSpeed แทน walkSpeed ในการเคลื่อนที่ ***
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Input กระโดด
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // ใช้แรงโน้มถ่วง
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        // === 3. การหมุนกล้องและตัวผู้เล่น ===
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.rotation *= Quaternion.Euler(0f, mouseX, 0f);

        rotationX -= mouseY; 
        rotationX = Mathf.Clamp(rotationX, lookDownLimit, lookUpLimit);
    }
    
    void LateUpdate()
    {
        if (Camera.main != null && Camera.main.transform.parent == transform)
        {
            Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        }
    }
}