using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class horsemove : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb2D; // Use Rigidbody2D for 2D games or Rigidbody for 3D games
    public humanmove human;
    public bool isGrounded;
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private PolygonCollider2D horseCollider; // Reference to the horse's collider

    public Slider jumpSlider; // Reference to the Slider UI element
    private float jumpChargeTime = 0f; // Time the space key is held
    public float maxJumpChargeTime = 2f; // Maximum time to charge the jump
    public float minJumpForce = 200f; // Minimum jump force
    public float maxJumpForce = 600f; // Maximum jump force
    public Transform checkpointlocation;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>(); // If using Rigidbody2D
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        horseCollider = GetComponent<PolygonCollider2D>(); // Get the horse's collider

        // Ensure the horse starts idle
        animator.SetBool("isIdle", true);
        human.isonhorse = false;

        // Initialize the jump slider
        jumpSlider.minValue = 0f;
        jumpSlider.maxValue = maxJumpChargeTime;
        jumpSlider.value = 0f;
        jumpSlider.gameObject.SetActive(false); // Hide the slider initially
    }

    void Update()
    {
        // Check if the horse is idle or moving
        bool isIdle = rb2D.velocity.magnitude < 0.1f;
        animator.SetBool("isIdle", isIdle);

        // Debugging
        Debug.Log("isIdle: " + isIdle);
        if (human.isonhorse == true)
        {
            // Show the jump slider
            jumpSlider.gameObject.SetActive(true);

            // Handle movement input
            HandleMovementHorse();
            HandleJumpingHorse();

            // Adjust the player's position relative to the horse
            human.transform.position = transform.position + (Vector3)human.offset;

            // Check for dismount input
            if (Input.GetKeyDown(KeyCode.F))
            {
                human.isonhorse = false;
                human.collider.size = human.standingsize; // Change collider size to standing size
                human.collider.offset = human.standingOffset; // Change collider offset to standing offset
                human.transform.SetParent(null); // Detach the player from the horse
                human.transform.position = transform.position + (Vector3)human.standingOffset; // Adjust position to avoid phasing
                human.animator.enabled = true; // Enable the animator
                jumpSlider.gameObject.SetActive(false); // Hide the jump slider
            }
        }
        else
        {
            // Detach the player from the horse when not mounted
            if (human.transform.parent == transform)
            {
                human.transform.SetParent(null);
                human.transform.position = transform.position + (Vector3)human.standingOffset; // Adjust position to avoid phasing
                human.animator.enabled = true; // Enable the animator
            }

            // Hide the jump slider
            jumpSlider.gameObject.SetActive(false);
        }
    }

    void HandleJumpingHorse()
    {
        // Check if the horse is on the ground
        if (isGrounded)
        {
            // Handle jump charging
            if (Input.GetKey(KeyCode.Space))
            {
                jumpChargeTime += Time.deltaTime;
                if (jumpChargeTime >= maxJumpChargeTime)
                {
                    jumpChargeTime = 0f; // Reset the charge time
                }
                jumpChargeTime = Mathf.Clamp(jumpChargeTime, 0f, maxJumpChargeTime);
                jumpSlider.value = jumpChargeTime;
            }

            // Handle jump release
            if (Input.GetKeyUp(KeyCode.Space))
            {
                float jumpForce = Mathf.Lerp(minJumpForce, maxJumpForce, jumpChargeTime / maxJumpChargeTime);
                rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpChargeTime = 0f;
                jumpSlider.value = 0f;
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // Check if the horse is touching an object with the "Ground" tag
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the horse is no longer touching an object with the "Ground" tag
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void HandleMovementHorse()
    {
        // Get input from the player
        float moveX = Input.GetAxis("Horizontal");

        // Create a movement vector
        Vector2 movement = new Vector2(moveX * human.moveSpeed, rb2D.velocity.y);

        // Apply the movement to the Rigidbody2D
        rb2D.velocity = movement;

        // Flip the sprite based on the direction of movement
        if (moveX > 0)
        {
            spriteRenderer.flipX = false; // Facing right
        }
        else if (moveX < 0)
        {
            spriteRenderer.flipX = true; // Facing left
        }
    }
}