using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health1Script : MonoBehaviour
{
    float playerhealth = 5;
    public Transform playerTransform;
    public Transform checkpoint1; // Checkpoint for Region 1
    public Transform checkpoint2; // Checkpoint for Region 2
    public Transform checkpoint3; // Checkpoint for Region 3
    public Transform checkpoint4; // Checkpoint for Region 4
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public int maxHealth = 5;
    public int currentHealth;
    private Rigidbody2D rb2D; // Reference to the player's Rigidbody2D component
    public float jumpPadForce = 10f; // Force applied by the Jumppad
    public RotateYcollectiones rotateYcollectiones;
    private int currentRegion = 0; // Tracks the current region (0 = default, 1 = region 1, 2 = region 2, 3 = region 3, 4 = region 4)

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHeartsUI();
        rb2D = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        // Check if the player collected something via RotateYcollectiones
        if (rotateYcollectiones.hascollectedplayer == true)
        {
            AddHealth(1);
            rotateYcollectiones.hascollectedplayer = false; // Reset the flag after adding health
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce health
        currentHealth = Mathf.Max(currentHealth, 0); // Ensure health doesn't go below 0
        UpdateHeartsUI(); // Update UI after taking damage
        if (currentHealth <= 0)
        {
            Die(); // Handle player death
        }
    }

    public void AddHealth(int health)
    {
        currentHealth += health; // Increase health
        currentHealth = Mathf.Min(currentHealth, maxHealth); // Ensure health doesn't exceed maxHealth
        UpdateHeartsUI(); // Update UI after adding health
    }

    void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    void Die()
    {
        // Respawn at the appropriate checkpoint based on the current region
        switch (currentRegion)
        {
            case 1:
                playerTransform.position = checkpoint1.position; // Move the player to checkpoint 1
                Debug.Log("Respawning at Checkpoint 1 (Region 1).");
                break;
            case 2:
                playerTransform.position = checkpoint2.position; // Move the player to checkpoint 2
                Debug.Log("Respawning at Checkpoint 2 (Region 2).");
                break;
            case 3:
                playerTransform.position = checkpoint3.position; // Move the player to checkpoint 3
                Debug.Log("Respawning at Checkpoint 3 (Region 3).");
                break;
            case 4:
                playerTransform.position = checkpoint4.position; // Move the player to checkpoint 4
                Debug.Log("Respawning at Checkpoint 4 (Region 4).");
                break;
            default:
                playerTransform.position = checkpoint1.position; // Default to checkpoint 1
                Debug.Log("Respawning at default Checkpoint 1.");
                break;
        }

        rb2D.velocity = Vector2.zero; // Reset the player's velocity
        ResetHearts();
    }

    void ResetHearts()
    {
        currentHealth = maxHealth; // Reset health to maximum
        UpdateHeartsUI(); // Update UI to show full hearts
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("rock"))
        {
            TakeDamage(1);
        }
        else if (collision.gameObject.CompareTag("void"))
        {
            TakeDamage(1);
        }
        else if (collision.gameObject.CompareTag("frog"))
        {
            TakeDamage(1);
        }
        else if (collision.gameObject.CompareTag("Jumppad"))
        {
            rb2D.AddForce(Vector2.up * jumpPadForce, ForceMode2D.Impulse);
        }
        else if (collision.gameObject.CompareTag("Hook"))
        {
            TakeDamage(5);
        }
        else if (collision.gameObject.CompareTag("saw"))
        {
            TakeDamage(1);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
                if (other.CompareTag("Region1"))
        {
            currentRegion = 1; // Set current region to 1
            Debug.Log("Player entered Region 1.");
        }
        else if (other.CompareTag("Region2"))
        {
            currentRegion = 2; // Set current region to 2
            Debug.Log("Player entered Region 2.");
        }
        else if (other.CompareTag("Region3"))
        {
            currentRegion = 3; // Set current region to 3
            Debug.Log("Player entered Region 3.");
        }
        else if (other.CompareTag("Region4"))
        {
            currentRegion = 4; // Set current region to 4
            Debug.Log("Player entered Region 4.");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Region1"))
        {
            currentRegion = 1; // Set current region to 1
            Debug.Log("Player entered Region 1.");
        }
        else if (other.CompareTag("Region2"))
        {
            currentRegion = 2; // Set current region to 2
            Debug.Log("Player entered Region 2.");
        }
        else if (other.CompareTag("Region3"))
        {
            currentRegion = 3; // Set current region to 3
            Debug.Log("Player entered Region 3.");
        }
        else if (other.CompareTag("Region4"))
        {
            currentRegion = 4; // Set current region to 4
            Debug.Log("Player entered Region 4.");
        }
        else if (other.CompareTag("Banana"))
        {
            AddHealth(1); // Add 1 health when collecting a banana
            Destroy(other.gameObject); // Destroy the banana after collection
            Debug.Log("Banana collected! Health increased.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Region1") || other.CompareTag("Region2") || other.CompareTag("Region3") || other.CompareTag("Region4"))
        {
            currentRegion = 0; // Reset to default region when exiting any region
            Debug.Log("Player exited a region.");
        }
    }
}