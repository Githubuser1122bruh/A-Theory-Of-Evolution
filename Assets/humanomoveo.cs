using UnityEngine;

public class humanomoveo : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rb2D; // Use Rigidbody2D for 2D games
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    public Transform normalplace;   // The position to place the object when not collected
    public float moveSpeed = 5f; // Movement speed
    public float jumpForce = 200f; // Jump force
    public bool isGrounded; // Check if the player is on the ground
    public BoxCollider2D collider;
    public bool hasEngine = false;
    public bool hasgas = false;
    public bool haswheels = false;
    public bool hasshell = false;
    private GameObject collectedObject; // Reference to the collected object
    public float throwForce = 10f; // Force applied when throwing the object
    public float jumpPadForce = 500f; // Force applied when hitting a jump pad
    public Transform player;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>(); // If using Rigidbody2D
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Handle collecting objects
        if (Input.GetMouseButtonDown(0))
        {
            CollectObject();
        }

        // Handle throwing collected object
        if (Input.GetMouseButtonDown(1) && collectedObject != null)
        {
            ThrowObject();
        }

        if (!collider.enabled)
        {
            collider.enabled = true;
        }

        // Handle movement input
        HandleMovement();

        // Handle jumping input
        HandleJumping();
    }

    void HandleMovement()
    {
        // Get input from the player
        float moveX = Input.GetAxis("Horizontal");

        // Create a movement vector
        Vector2 movement = new Vector2(moveX * moveSpeed, rb2D.velocity.y);

        // Apply the movement to the Rigidbody2D
        rb2D.velocity = movement;

        // Update the animator's Humantrigger parameter
        animator.SetBool("Humantrigger", Mathf.Abs(moveX) > 0);

        // Flip the sprite based on the direction of movement
        if (moveX > 0)
        {
            spriteRenderer.flipX = true; // Facing right
        }
        else if (moveX < 0)
        {
            spriteRenderer.flipX = false; // Facing left
        }
    }

    void HandleJumping()
    {
        // Jump if the player is on the ground and the jump button is pressed
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false; // Player is now in the air
            animator.SetBool("isJumping", true); // Trigger jumping animation
        }

        // Update the animator's isJumping parameter
        if (isGrounded)
        {
            animator.SetBool("isJumping", false); // Reset jumping animation when grounded
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // Check if the player is touching an object with the "Ground" tag
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the player stopped touching an object with the "Ground" tag
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player hits a jump pad
        if (collision.gameObject.CompareTag("Jumppad"))
        {
            rb2D.velocity = Vector2.zero; // Reset velocity to ensure consistent bounce
            rb2D.AddForce(new Vector2(0f, jumpPadForce), ForceMode2D.Impulse); // Apply upward force
            animator.SetBool("isJumping", true); // Trigger jumping animation
        }
        if (collision.gameObject.CompareTag("Engine"))
        {
            hasEngine = true;
            collision.gameObject.SetActive(false);
            player.transform.position = normalplace.position;
        }
        if (collision.gameObject.CompareTag("Gas"))
        {
            hasgas = true;
            collision.gameObject.SetActive(false);
            player.transform.position = normalplace.position;
        }
        if (collision.gameObject.CompareTag("Wheels"))
        {
            haswheels = true;
            collision.gameObject.SetActive(false);
            player.transform.position = normalplace.position;
        }
        if (collision.gameObject.CompareTag("Shell"))
        {
            hasshell = true;
            collision.gameObject.SetActive(false);
            player.transform.position = normalplace.position;
        }
    }

    void CollectObject()
    {
        // Raycast to detect objects under the mouse cursor
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Collectible"))
        {
            collectedObject = hit.collider.gameObject;
            collectedObject.SetActive(false); // Deactivate the collected object
        }
    }

    void ThrowObject()
    {
        collectedObject.SetActive(true); // Reactivate the collected object
        collectedObject.transform.position = transform.position; // Set the position to the player's position

        // Calculate the direction to throw the object
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 throwDirection = (mousePosition - (Vector2)transform.position).normalized;

        Rigidbody2D collectedRb2D = collectedObject.GetComponent<Rigidbody2D>();
        if (collectedRb2D != null)
        {
            collectedRb2D.velocity = Vector2.zero; // Reset the velocity
            collectedRb2D.AddForce(throwDirection * throwForce, ForceMode2D.Impulse); // Apply force to throw the object
        }

        collectedObject = null; // Clear the reference to the collected object
    }
}