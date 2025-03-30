using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthcar : MonoBehaviour
{
    public int maxHealth = 5; // Maximum health
    public int currentHealth; // Current health

    public Image[] hearts; // Array of heart UI images
    public Sprite fullHeart; // Sprite for a full heart
    public Sprite emptyHeart; // Sprite for an empty heart

    public Transform defaultRespawnPoint; // Default respawn point
    public Transform region1RespawnPoint; // Respawn point for region 1
    public Transform region2RespawnPoint; // Respawn point for region 2
    public Transform region3RespawnPoint; // Respawn point for region 3
    public Transform region4RespawnPoint; // Respawn point for region 4
    public Transform region5RespawnPoint; // Respawn point for region 5

    private int currentRegion = 0; // Tracks the current region (0 = default, 1 = region 1, 2 = region 2, 3 = region 3, 4 = region 4, 5 = region 5)
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private float defaultGravityScale; // Store the default gravity scale

    public bool hasknife = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Initialize health to max
        UpdateHeartsUI(); // Update the hearts UI

        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        if (rb != null)
        {
            defaultGravityScale = rb.gravityScale; // Store the default gravity scale
        }

        Debug.Log("Player initialized with max health: " + maxHealth);
    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't go below 0
        UpdateHeartsUI(); // Update the hearts UI

        Debug.Log("Player took damage: " + damage + ", Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Respawn(); // Respawn the player when health reaches 0
        }
    }

    // Method to heal
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't exceed maxHealth
        UpdateHeartsUI(); // Update the hearts UI

        Debug.Log("Player healed: " + amount + ", Current health: " + currentHealth);
    }

    // Update the hearts UI
    void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart; // Set to full heart
            }
            else
            {
                hearts[i].sprite = emptyHeart; // Set to empty heart
            }

            // Enable or disable the heart image based on maxHealth
            hearts[i].enabled = i < maxHealth;
        }

        Debug.Log("Hearts UI updated. Current health: " + currentHealth);
    }

    // Respawn the player
    void Respawn()
    {
        Debug.Log("Player has died. Respawning...");

        // Respawn at the appropriate checkpoint based on the current region
        switch (currentRegion)
        {
            case 1:
                transform.position = region1RespawnPoint.position;
                Debug.Log("Respawning at Region 1 checkpoint.");
                break;
            case 2:
                transform.position = region2RespawnPoint.position;
                Debug.Log("Respawning at Region 2 checkpoint.");
                break;
            case 3:
                transform.position = region3RespawnPoint.position;
                Debug.Log("Respawning at Region 3 checkpoint.");
                break;
            case 4:
                transform.position = region4RespawnPoint.position;
                Debug.Log("Respawning at Region 4 checkpoint.");
                break;
            case 5:
                transform.position = region5RespawnPoint.position;
                Debug.Log("Respawning at Region 5 checkpoint.");
                break;
            default:
                transform.position = defaultRespawnPoint.position; // Default respawn point
                Debug.Log("Respawning at default checkpoint.");
                break;
        }

        currentHealth = maxHealth; // Reset health to max
        UpdateHeartsUI(); // Update the hearts UI
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("rock"))
        {
            TakeDamage(1); // Take 1 damage
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            TakeDamage(1);
        }
        else if (other.gameObject.CompareTag("frog"))
        {
            TakeDamage(1);
        }
        else if (other.gameObject.CompareTag("void"))
        {
            TakeDamage(5);
        }
        else if (other.gameObject.CompareTag("Rat"))
        {
            TakeDamage(1);
        }
        else if (other.gameObject.CompareTag("Water"))
        {
            TakeDamage(2);
        }
        else if (other.gameObject.CompareTag("Health"))
        {
            Heal(1);
            Destroy(other.gameObject); // Destroy the health pickup
        }
        else if (other.gameObject.CompareTag("saw"))
        {
            Destroy(other.gameObject); // Destroy the saw obstacle\
            hasknife = true;
        }
        else if (other.gameObject.CompareTag("grenade"))
        {
            TakeDamage(1);

            // Apply a force to launch the player back at a 45-degree angle
            if (rb != null)
            {
                // Calculate the launch direction at a 45-degree angle
                Vector2 grenadePosition = other.transform.position;
                Vector2 playerPosition = transform.position;

                Vector2 direction = (playerPosition - grenadePosition).normalized; // Direction away from the grenade
                Vector2 launchDirection = new Vector2(direction.x, Mathf.Abs(direction.x)); // Adjust to create a 45-degree angle

                float launchForce = 15f; // Adjust the force as needed
                rb.AddForce(launchDirection.normalized * launchForce, ForceMode2D.Impulse);

                Debug.Log("Player launched back by grenade at a 45-degree angle.");
            }

            Destroy(other.gameObject); // Destroy the grenade
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // Continuously check which region the player is in
        if (other.CompareTag("Region1"))
        {
            currentRegion = 1;
            Debug.Log("Player is in Region 1.");
        }
        else if (other.CompareTag("Region2"))
        {
            currentRegion = 2;
            Debug.Log("Player is in Region 2.");

            // Slow falling in Region 2
            if (rb != null)
            {
                rb.gravityScale = defaultGravityScale / 3f; // Reduce gravity scale for slow falling

                // Reset vertical velocity to prevent fast falling
                if (Mathf.Abs(rb.velocity.y) > 5f) // Threshold for velocity
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0f); // Reset vertical velocity
                    Debug.Log("Vertical velocity reset in Region 2.");
                }
            }
        }
        else if (other.CompareTag("Region3"))
        {
            currentRegion = 3;
            Debug.Log("Player is in Region 3.");
        }
        else if (other.CompareTag("Region4"))
        {
            currentRegion = 4;
            Debug.Log("Player is in Region 4.");
        }
        else if (other.CompareTag("Region5"))
        {
            currentRegion = 5;
            Debug.Log("Player is in Region 5.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Reset to default region when exiting any region
        if (other.CompareTag("Region1") || other.CompareTag("Region2") || other.CompareTag("Region3") || other.CompareTag("Region4") || other.CompareTag("Region5"))
        {
            Debug.Log("Player exited Region " + currentRegion);
            currentRegion = 0;

            // Reset gravity scale when exiting Region 2
            if (rb != null && other.CompareTag("Region2"))
            {
                rb.gravityScale = defaultGravityScale; // Reset to default gravity scale
                Debug.Log("Gravity scale reset to default.");
            }
        }
    }
}