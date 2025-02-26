using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Transform cameraController;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 5f;

    private bool isGrounded = false;
    private int jumpCounter = 0;

    private Rigidbody rb;

    private void Start()
    {
        inputManager.OnMove.AddListener(MovePlayer);
        inputManager.OnSpacePressed.AddListener(() => Jump(Vector3.up));

        rb = GetComponent<Rigidbody>();
    }

    private void MovePlayer(Vector2 direction)
    {
        Vector3 moveDirection = cameraController.forward * direction.y + cameraController.right * direction.x;
        moveDirection.y = 0f; // Ensure no vertical movement

        rb.linearVelocity = moveDirection.normalized * speed + new Vector3(0, rb.linearVelocity.y, 0);
    }



    private void Jump(Vector3 direction) {
        if (isGrounded || jumpCounter < 2)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // Reset Y velocity for consistent jumps
            rb.AddForce(direction * jumpForce, ForceMode.Impulse);
            jumpCounter++;
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCounter = 0;
        }
    }
}
