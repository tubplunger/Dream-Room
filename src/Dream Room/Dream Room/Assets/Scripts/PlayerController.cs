using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 100f;
    public Transform playerCamera;

    private Rigidbody rb;
    private float xRotation = 0f;

    private float xInput;
    private float zInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // no tipping
        rb.freezeRotation = true;
    }

    void Update()
    {
        HandleMouseLook();
        GetInput();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void GetInput()
    {
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");
    }

    void HandleMovement()
    {
        Vector3 move = transform.right * xInput + transform.forward * zInput;

        Vector3 velocity = move * moveSpeed;
        velocity.y = rb.velocity.y;

        rb.velocity = velocity;
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }
}
