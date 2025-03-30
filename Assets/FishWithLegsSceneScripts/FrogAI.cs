using UnityEngine;
using System.Collections;

public class FrogAI : MonoBehaviour
{
    public float jumpForce = 5f;  // How high the frog jumps
    public float moveForce = 3f;  // Horizontal movement force
    public float jumpIntervalMin = 1f;  // Minimum time between jumps
    public float jumpIntervalMax = 2.5f;  // Maximum time between jumps

    private Rigidbody2D rb;
    private Transform player;
    private bool isGrounded = true;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;  // Finds the player by tag
        StartCoroutine(JumpRoutine());
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;  // Reset jump when landing
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Deal damage to player
            HealthScript playerHealth = collision.gameObject.GetComponent<HealthScript>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
        }
    }

    IEnumerator JumpRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(jumpIntervalMin, jumpIntervalMax);
            yield return new WaitForSeconds(waitTime);

            if (isGrounded)
            {
                Jump();
            }
        }
    }

    void Jump()
    {
        isGrounded = false;

        Vector2 jumpDirection;
        if (player != null)
        {
            // Get direction towards player
            float direction = Mathf.Sign(player.position.x - transform.position.x);
            jumpDirection = new Vector2(direction * moveForce, jumpForce);
            if (jumpDirection.x < 0)
            {
                // Flip sprite if moving left
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            // Default random hopping if no player found
            float randomDirection = Random.Range(-1f, 1f);
            jumpDirection = new Vector2(randomDirection * moveForce, jumpForce);
        }

        rb.velocity = jumpDirection;
    }
}
