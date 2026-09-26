using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private float speed = 5f;
    public Rigidbody rb;
    private Vector3 moveDirection;
    private bool isMoving = false;
    private bool isJumping = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();        
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);

        moveDirection = Vector3.zero;
        isMoving = false;

        if (Keyboard.current.wKey.isPressed)
        {
            moveDirection += transform.forward;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            moveDirection -= transform.forward;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            moveDirection -= transform.right;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            moveDirection += transform.right;
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !isJumping)
        {
            isJumping = true;
        }

        if (moveDirection != Vector3.zero)
        {
            moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);
            isMoving = true;
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.linearVelocity = moveDirection * speed;
        }

        if (isJumping)
        {
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            isJumping = false;
        }
    }
}
