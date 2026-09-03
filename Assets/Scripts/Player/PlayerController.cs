using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float forwardJumpDistance = 3f;
    [SerializeField] float jumpHeight = 1.2f;          // Maximum peak Y height
    [SerializeField] float jumpDuration = 0.6f;       // Total time in seconds to complete jump
    [SerializeField] float xClamp = 3f;
    [SerializeField] float zClamp = 3f;
    [SerializeField] Animator animator;

    [Header("Ground Check Settings")]
    [SerializeField] float rayDistance = 1.2f;
    [SerializeField] LayerMask groundLayer;

    const string jumpTrigger = "Jump";

    Vector2 movement;
    Rigidbody rigidbody;

    bool isGrounded;
    bool isJumping;
    float jumpTimer;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, rayDistance, groundLayer);

        if (isJumping)
        {
            jumpTimer -= Time.deltaTime;
            if (jumpTimer <= 0f)
            {
                isJumping = false;
            }
        }
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded && !isJumping)
        {
            if (animator != null)
            {
                // Clear any lingering buffered trigger so it doesn't fire late
                animator.ResetTrigger(jumpTrigger);
                animator.SetTrigger(jumpTrigger);

                // Alternative (forces instant playback without transition delay):
                animator.Play("JumpStateName", 0, 0f);
            }
            isJumping = true;
            jumpTimer = jumpDuration;
        }
    }

    void HandleMovement()
    {
        Vector3 currentPosition = rigidbody.position;

        float currentZBoost = 0f;
        float targetY = 0f; // Default ground level

        if (isJumping)
        {
            // Progress goes from 0.0 at jump start to 1.0 at jump end
            float normalizedProgress = 1f - (jumpTimer / jumpDuration);
            
            // Forward boost
            currentZBoost = forwardJumpDistance;

            // Sine arc: Sin(0) = 0 -> Sin(PI / 2) = 1 (Peak) -> Sin(PI) = 0 (Ground)
            targetY = Mathf.Sin(normalizedProgress * Mathf.PI) * jumpHeight;
        }

        Vector3 moveDirection = new Vector3(movement.x, 0f, movement.y + currentZBoost);
        Vector3 newPosition = currentPosition + moveDirection * (moveSpeed * Time.fixedDeltaTime);

        // Directly set the Y height based on the arc formula
        newPosition.y = targetY;

        newPosition.x = Mathf.Clamp(newPosition.x, -xClamp, xClamp);
        newPosition.z = Mathf.Clamp(newPosition.z, -zClamp, zClamp);

        rigidbody.MovePosition(newPosition);
    }
}