using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMove : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed at which the rat moves
    public float moveDuration = 2f; // Duration for which the rat moves in one direction
    public float idleDuration = 1f; // Duration for which the rat stays idle

    private float moveTimer;
    private float idleTimer;
    private bool isMoving = false;
    private int direction = 1; // 1 for right, -1 for left

    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        idleTimer = idleDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            MoveRat();
        }
        else
        {
            IdleRat();
        }
    }

    void MoveRat()
    {
        moveTimer -= Time.deltaTime;
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);

        if (moveTimer <= 0)
        {
            isMoving = false;
            idleTimer = idleDuration;
            rb.velocity = Vector2.zero;
            animator.SetFloat("RatTrigger", 0); // Set to idle animation
        }

        // Flip the sprite based on the direction
        if (direction > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (direction < 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void IdleRat()
    {
        idleTimer -= Time.deltaTime;

        if (idleTimer <= 0)
        {
            isMoving = true;
            moveTimer = moveDuration;
            direction = Random.Range(0, 2) == 0 ? -1 : 1; // Randomly choose direction
            animator.SetFloat("RatTrigger", 1); // Set to moving animation
        }
    }
}