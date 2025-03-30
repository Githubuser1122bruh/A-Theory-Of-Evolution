using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movementscript : MonoBehaviour
{
    public float moveSpeed = 5f; // Normal movement speed
    public float sprintSpeed = 8f; // Sprint movement speed
    public float jumpForce = 7f; // Jump strength
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isGrounded;
    private SpriteRenderer spriteRenderer;
    public Sprite standingSprite; // Default sprite
    public Sprite crouchingSprite;
    public BoxCollider2D Collider;
    public bool isCrouching = false;
    public Vector2 offsetcrouch;
    public Vector2 offsetstand;
    public float iceFriction = 0.98f;
    public PhysicsMaterial2D normalMaterial; // Reference to normal ground material
    public PhysicsMaterial2D iceMaterial; // Reference to ice material
    public Vector2 crouchingsize;
    public Vector2 standingsize;

    private float terrainSpeedModifier = 1f; // Speed modifier for different surfaces

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = standingSprite;
    }

    void Update()
    {
        // Get input for movement
        movement.x = Input.GetAxisRaw("Horizontal");

        // Handle Crouching
        if (Input.GetKey(KeyCode.S))
        {
            isCrouching = true;
            spriteRenderer.sprite = crouchingSprite;
            Collider.offset = offsetcrouch;
            Collider.enabled = true; // Ensure collider is enabled
            Collider.size = crouchingsize;
        }
        else
        {
            isCrouching = false;
            Collider.offset = offsetstand;
            Collider.enabled = true; // Ensure collider is enabled
            spriteRenderer.sprite = standingSprite;
            Collider.size = standingsize;
        }

        // Flip the sprite based on direction
        if (movement.x < 0)
            spriteRenderer.flipX = false; // Face left
        else if (movement.x > 0)
            spriteRenderer.flipX = true; // Face right

        // Jumping
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false; // Prevents double jumping
        }
    }

    void FixedUpdate()
    {
        // Apply movement (horizontal only)
        float baseSpeed = moveSpeed * terrainSpeedModifier;

        // Handle Sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            baseSpeed = sprintSpeed * terrainSpeedModifier;
        }

        rb.velocity = new Vector2(movement.x * baseSpeed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            terrainSpeedModifier = 1f; // Normal speed
        }
        else if (collision.gameObject.CompareTag("sand"))
        {
            terrainSpeedModifier = 0.5f; // Slow down on sand
        }
        else if (collision.gameObject.CompareTag("Icezone"))
        {
            terrainSpeedModifier = 1.7f; // Speed up on ice
        }
        
        else
        {
            terrainSpeedModifier = 1f; // Default speed
        }
        
    }

}
