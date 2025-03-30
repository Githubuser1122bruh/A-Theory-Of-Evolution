using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkChase : MonoBehaviour
{
    public float speed = 5.0f;
    public Transform playerTransform;
    private bool isChasing = false;
    public Transform sharkResetPoint;
    public Transform checkpointTransform;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        if (isChasing && playerTransform != null)
        {
            

            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle + 180);
            }
        }
    }

    public void StartChase(Transform player)
    {
        playerTransform = player;
        isChasing = true;
    }

    public void StopChase()
    {
        isChasing = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player caught by the shark!");

            // Reset player position
            if (checkpointTransform != null)
            {
                playerTransform.position = checkpointTransform.position;
            }
            else
            {
                Debug.LogError("CheckpointTransform is not assigned in the inspector!");
            }

            // Reset shark position
            ResetShark();
        }
    }

    void ResetShark()
    {
        if (sharkResetPoint != null)
        {
            transform.position = sharkResetPoint.position; // Move shark back
            isChasing = false; // Stop the chase
            
            if (rb != null)
            {
                rb.velocity = Vector2.zero; // Stop movement completely
            }
        }
        else
        {
            Debug.LogError("SharkResetPoint is not assigned in the inspector!");
        }
    }
}
