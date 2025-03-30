using UnityEngine;

public class humanmove : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rb2D; // Use Rigidbody2D for 2D games
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    public float moveSpeed = 5f; // Movement speed
    public float jumpForce = 200f; // Default jump force
    public float jumpForceOnHorse = 300f; // Jump force when on the horse
    public bool isGrounded; // Check if the player is on the ground
    public bool isonhorse = false; // Check if the player is on the horse
    public Transform first_object; // Player
    public Transform second_object; // Horse
    public float detectionrange; // Distance between player and horse
    public Vector2 offset = new Vector2(0, 0); // Offset when mounting the horse
    public Vector2 sittingsize; // Size of the collider when sitting
    public Vector2 standingsize; // Size of the collider when standing
    public Vector2 sittingOffset; // Offset of the collider when sitting
    public Vector2 standingOffset; // Offset of the collider when standing
    public BoxCollider2D collider;
    public Sprite sittingSprite; // Reference to the sitting sprite
    public Sprite standingSprite; // Reference to the standing sprite
    public bool ZoneTrigger = false; // Check if the player is in the trigger zone

    private GameObject collectedObject; // Reference to the collected object
    public float throwForce = 10f; // Force applied when throwing the object

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>(); // If using Rigidbody2D
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
    }

    void Update()
    {
        // Handle movement input
        HandleMovement();

        // Handle jumping input
        HandleJumping();

        // Handle mounting and dismounting the horse
        HandleHorseClimbing();

        // Throw the collected object if the player presses the C key
        if (Input.GetKeyDown(KeyCode.C))
        {
            ThrowObject();
        }
    }

    void HandleMovement()
    {
        // Get input from the player
        float moveX = Input.GetAxis("Horizontal");

        // Create a movement vector
        Vector2 movement = new Vector2(moveX * moveSpeed, rb2D.velocity.y);

        // Apply the movement to the Rigidbody2D
        rb2D.velocity = movement;

        // Flip the sprite based on the direction of movement
        if (moveX < 0)
        {
            spriteRenderer.flipX = false; // Facing right
        }
        else if (moveX > 0)
        {
            spriteRenderer.flipX = true; // Facing left
        }
    }

    void HandleJumping()
    {
        // Jump if the player is on the ground and the jump button is pressed
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            float currentJumpForce = isonhorse ? jumpForceOnHorse : jumpForce;
            rb2D.AddForce(new Vector2(0f, currentJumpForce), ForceMode2D.Impulse);
        }
    }

    void HandleHorseClimbing()
    {
        // Calculate the distance between the player and the horse
        detectionrange = Vector2.Distance(first_object.position, second_object.position);

        // Mount the horse if the player presses E and is within range
        if (Input.GetKeyDown(KeyCode.E) && detectionrange <= 5)
        {
            isonhorse = true;
            first_object.transform.position = second_object.transform.position + (Vector3)offset;
            spriteRenderer.sprite = sittingSprite; // Change to sitting sprite
            collider.size = sittingsize; // Change collider size to sitting size
            collider.offset = sittingOffset; // Change collider offset to sitting offset
            animator.enabled = false; // Disable the animator
        }
        // Dismount the horse if the player presses F
        else if (Input.GetKeyDown(KeyCode.F) && isonhorse)
        {
            isonhorse = false;
            spriteRenderer.sprite = standingSprite; // Change to standing sprite
            collider.size = standingsize; // Change collider size to standing size
            collider.offset = standingOffset; // Change collider offset to standing offset
            animator.enabled = true; // Enable the animator
            first_object.transform.position = new Vector3(second_object.transform.position.x, second_object.transform.position.y + standingOffset.y, second_object.transform.position.z); // Adjust position to avoid phasing
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collides with an object tagged "Collectible"
        if (collision.gameObject.CompareTag("Collectible"))
        {
            collectedObject = collision.gameObject;
            collectedObject.SetActive(false); // Deactivate the collected object
            Debug.Log("Collected object: " + collectedObject.name); // Debug log to confirm collection
        }

        // Check if the player is touching the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the player stopped touching the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void ThrowObject()
    {
        if (collectedObject == null) return;

        collectedObject.SetActive(true); // Reactivate the collected object

        // Calculate the direction to throw the object towards the cursor
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Get the cursor position in world space
        Vector2 throwDirection = (mousePosition - (Vector2)transform.position).normalized; // Calculate the normalized direction

        // Position the object slightly in front of the player in the throw direction
        collectedObject.transform.position = (Vector2)transform.position + throwDirection * 1f; // Offset by 0.5 units

        Rigidbody2D collectedRb2D = collectedObject.GetComponent<Rigidbody2D>();
        if (collectedRb2D != null)
        {
            collectedRb2D.velocity = Vector2.zero; // Reset the velocity
            collectedRb2D.AddForce(throwDirection * throwForce, ForceMode2D.Impulse); // Apply force to throw the object
        }

        collectedObject = null; // Clear the reference to the collected object
    }
}