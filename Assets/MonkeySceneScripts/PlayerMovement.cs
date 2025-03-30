using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;
    float moveSpeed = 5f; // Normal movement speed
    float climbSpeed = 2f; // Climbing speed (slower than normal movement speed)
    bool isFacingRight = false;
    bool isGrounded = false;
    bool isClimbing = false;
    bool canClimb = false;
    Collider2D nearestVine; // Track the closest vine dynamically

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    BoxCollider2D playerCollider; // Reference to the player's BoxCollider2D

    public float climbVicinityRadius = 1f; // Vicinity around vines
    public LayerMask vineLayer; // Vine layer for detection

    public float jumpForce = 7f; // Jump force for the player
    public Sprite climbingSprite; // Sprite for climbing animation
    public Sprite defaultSprite; // Default sprite

    public Vector2 climbingColliderSize = new Vector2(0.5f, 2f); // The size of the collider during climbing
    public Vector2 defaultColliderSize = new Vector2(1f, 2f); // The default size of the collider

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Initialize the SpriteRenderer
        playerCollider = GetComponent<BoxCollider2D>(); // Initialize the player's BoxCollider2D
    }

    void Update()
    {
        if (isClimbing)
        {
            HandleClimbingMovement();
        }
        else
        {
            HandleNormalMovement();
        }

        FlipSprite();
    }

    void HandleClimbingMovement()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        // Restrict the movement to the vine's bounds
        if (nearestVine != null)
        {
            // Get vine bounds
            float vineLeftEdge = nearestVine.bounds.min.x;
            float vineRightEdge = nearestVine.bounds.max.x;
            float vineBottomEdge = nearestVine.bounds.min.y;
            float vineTopEdge = nearestVine.bounds.max.y;

            // Calculate new position with input
            float newX = Mathf.Clamp(
                transform.position.x + horizontalInput * climbSpeed * Time.deltaTime,
                vineLeftEdge,
                vineRightEdge
            );
            float newY = Mathf.Clamp(
                transform.position.y + verticalInput * climbSpeed * Time.deltaTime,
                vineBottomEdge,
                vineTopEdge
            );

            // Apply the clamped position
            transform.position = new Vector2(newX, newY);

            // Update velocity for animation (optional)
            rb.velocity = new Vector2(horizontalInput * climbSpeed, verticalInput * climbSpeed);
        }

        animator.SetBool("isClimbing", true);
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", Mathf.Abs(rb.velocity.y));

        // Allow the player to drop from the vine by pressing 'F'
        if (Input.GetKeyDown(KeyCode.F)) // Exit climbing
        {
            DropFromVine();
        }
    }

    void HandleNormalMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetBool("isJumping", !isGrounded);

        // Debug log to check if the player is grounded or jumping
        if (isGrounded)
        {
            Debug.Log("Player is grounded during normal movement.");
        }
        else
        {
            Debug.Log("Player is jumping during normal movement.");
        }

        // Jump Mechanic
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (canClimb && Input.GetKeyDown(KeyCode.E))
        {
            StartClimbing();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;
        animator.SetBool("isJumping", true);
    }

    void FlipSprite()
    {
        // Flip the sprite based on horizontal input
        if (horizontalInput < 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizontalInput > 0 && isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        // Switch the direction the player is facing
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
            Debug.Log("Player is grounded."); // Debug log to confirm the player is grounded
        }
        else if (collision.CompareTag("Vine"))
        {
            if (IsWithinVicinity(collision))
            {
                canClimb = true;
                nearestVine = collision; // Track the closest vine
                Debug.Log("Player is near a vine and can climb."); // Debug log for climbing detection
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("Player is no longer grounded."); // Debug log to confirm the player is not grounded
        }
        else if (collision.CompareTag("Vine"))
        {
            if (nearestVine == collision)
            {
                canClimb = false;
                nearestVine = null;
                Debug.Log("Player left the vicinity of the vine."); // Debug log for leaving the vine
            }
        }
    }

    private void DropFromVine()
    {
        isClimbing = false;
        rb.gravityScale = 1; // Re-enable gravity
        animator.enabled = true; // Re-enable animator for normal animations
        spriteRenderer.sprite = defaultSprite; // Revert to the default sprite

        // Reset animator parameters to default values
        animator.SetBool("isClimbing", false);
        animator.SetFloat("xVelocity", 0);
        animator.SetFloat("yVelocity", 0);
        animator.SetBool("isJumping", !isGrounded); // Reset jumping state based on grounded status

        // Debug log to check grounded status after dropping from the vine
        Debug.Log("Player dropped from the vine. Grounded: " + isGrounded);

        // Revert to the original collider size
        playerCollider.size = defaultColliderSize;

        // Reset the player's velocity to ensure they don't continue moving sideways
        rb.velocity = new Vector2(0, 0);

        // Ensure the player is grounded after dropping
        isGrounded = Physics2D.OverlapCircle(transform.position, 0.1f, vineLayer) == null;
        Debug.Log("Grounded status after dropping from vine: " + isGrounded); // Debug log for grounded status
    }

    private bool IsWithinVicinity(Collider2D vine)
    {
        return Vector2.Distance(transform.position, vine.transform.position) <= climbVicinityRadius;
    }

    private void StartClimbing()
    {
        isClimbing = true;
        rb.gravityScale = 0; // Disable gravity while climbing
        rb.velocity = Vector2.zero; // Reset velocity
        animator.enabled = true; // Enable climbing animations
        spriteRenderer.sprite = climbingSprite; // Change to climbing sprite
        playerCollider.size = climbingColliderSize; // Adjust collider size for climbing
        Debug.Log("Player started climbing."); // Debug log for climbing
    }
}