using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class airpush : MonoBehaviour
{
    public float pushForce = 5f; // The upward force applied to the player

    // Called when another collider stays within the trigger collider
    void OnTriggerStay2D(Collider2D other)
    {
        // Check if the object has a Rigidbody2D (e.g., the player)
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Apply an upward force to the object
            rb.AddForce(Vector2.up * pushForce, ForceMode2D.Force);
        }
    }
}