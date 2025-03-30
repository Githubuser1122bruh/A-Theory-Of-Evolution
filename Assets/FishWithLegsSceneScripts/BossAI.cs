using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class BossAI : MonoBehaviour
{
    public float jumpForce = 8f;
    public float jumpCooldown = 2f;
    public float moveSpeed = 2f; // Speed to follow the player
    public Transform player; // Assign the player transform in the Inspector
    public bool isSecondPhase;
    public bool running = false;
    public Slider healthbar;
    public float bosshealth = 100f;
    private Rigidbody2D rb;
    private float nextJumpTime;
    private SpriteRenderer spriteRenderer;
    public GameObject canvas;
    public GameObject dropItemPrefab; 
    public HealthScript healthScript;


    void Start()
    {
        StartCoroutine(SecondPhase());
        rb = GetComponent<Rigidbody2D>();
        nextJumpTime = Time.time;
        spriteRenderer = GetComponent<SpriteRenderer>();
        canvas.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        bosshealth -= damage;
        if (bosshealth <= 0)
        {
            Destroy(gameObject);
            canvas.SetActive(false);
            Instantiate(dropItemPrefab, transform.position, Quaternion.identity);
        }
        if (bosshealth <= 50)
        {
            SecondPhase();
        }

    }
    void Update()
    {
        if(running) 
        {
            FollowPlayer();
            if (Time.time > nextJumpTime)
            {
                Jump();
            }
            FlipSprite();
            if (healthbar != null)
            {
                healthbar.value = bosshealth;
            }
            else
            {
                Debug.LogError("HealthSlider not found!");
            }
        }

    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        nextJumpTime = Time.time + jumpCooldown;
    }

    void FollowPlayer()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
    }

    void FlipSprite()
    {
        if (player == null) return;

        if (player.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (player.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
    }
    IEnumerator SecondPhase()
        {
            isSecondPhase = true;
            while (isSecondPhase)
            {
                bosshealth += 5;
                yield return new WaitForSeconds(3f); // Adjust the wait time as needed
                jumpCooldown = 3f;
                jumpForce = 5f;
            }
        }


    
}