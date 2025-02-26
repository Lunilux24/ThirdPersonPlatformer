using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CinemachineCamera freeLookCamera;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 5f;

    private bool isGrounded = false;
    private int jumpCounter = 0;

    private Rigidbody rb;

    private void Start()
    {
        inputManager.OnMove.AddListener(MovePlayer);
        inputManager.OnSpacePressed.AddListener(() => Jump(Vector3.up));
        inputManager.OnLook.AddListener(RotateCamera);

        rb = GetComponent<Rigidbody>();
    }

    private void MovePlayer(Vector2 direction)
    {
        Vector3 moveDirection = new(direction.x, 0f, direction.y);
        rb.AddForce(speed * moveDirection);
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

    private void RotateCamera(Vector2 lookInput)
    {
        if (freeLookCamera != null)
        {
            transform.forward = freeLookCamera.transform.forward;
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }
}
