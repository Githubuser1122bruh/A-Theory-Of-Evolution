using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class horsehealth : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public int maxHealth = 3;
    public int currentHealth;
    public Transform checkpoint1; // Checkpoint for Region 1
    public Transform checkpoint2; // Checkpoint for Region 2
    public Transform checkpoint3; // Checkpoint for Region 3
    public Transform checkpoint4; // Checkpoint for Region 4
    public humanmove player; // Reference to the player script
    public Vector2 dismountOffset = new Vector2(0, -1); // Offset to apply when dismounting
    public float jumpPadForce = 10f; // Force applied by the Jumppad
    private Rigidbody2D rb2D; // Reference to the horse's Rigidbody2D component
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
        if (rotateYcollectiones.hascollectedhorse == true)
        {
            AddHealth(1);
            rotateYcollectiones.hascollectedhorse = false; // Reset the flag after adding health
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce health
        currentHealth = Mathf.Max(currentHealth, 0); // Ensure health doesn't go below 0
        UpdateHeartsUI(); // Update UI after taking damage
        if (currentHealth <= 0)
        {
            Die(); // Handle horse death
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
        if (player.isonhorse)
        {
            player.isonhorse = false;
            player.transform.SetParent(null); // Detach the player from the horse
            player.transform.position = new Vector2(transform.position.x, transform.position.y) + player.standingOffset + dismountOffset; // Adjust position to avoid phasing
            player.animator.enabled = true; // Enable the animator
            player.collider.size = player.standingsize; // Change collider size to standing size
            player.collider.offset = player.standingOffset; // Change collider offset to standing offset
            player.collider.enabled = true; // Ensure the player's collider is enabled
        }

        // Respawn at the appropriate checkpoint based on the current region
        switch (currentRegion)
        {
            case 1:
                transform.position = checkpoint1.position; // Move the horse to checkpoint 1
                Debug.Log("Respawning at Checkpoint 1 (Region 1).");
                break;
            case 2:
                transform.position = checkpoint2.position; // Move the horse to checkpoint 2
                Debug.Log("Respawning at Checkpoint 2 (Region 2).");
                break;
            case 3:
                transform.position = checkpoint3.position; // Move the horse to checkpoint 3
                Debug.Log("Respawning at Checkpoint 3 (Region 3).");
                break;
            case 4:
                transform.position = checkpoint4.position; // Move the horse to checkpoint 4
                Debug.Log("Respawning at Checkpoint 4 (Region 4).");
                break;
            default:
                transform.position = checkpoint1.position; // Default to checkpoint 1
                Debug.Log("Respawning at default Checkpoint 1.");
                break;
        }

        rb2D.velocity = Vector2.zero; // Reset the horse's velocity
        ResetHearts();
    }

    void ResetHearts()
    {
        currentHealth = maxHealth; // Reset health to maximum
        UpdateHeartsUI(); // Update UI to show full hearts
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("rock"))
        {
            TakeDamage(1);
            // Ensure the rock does not become part of the horse
            collision.transform.SetParent(null);
        }
        else if (collision.gameObject.CompareTag("void"))
        {
            TakeDamage(1);
        }
        else if (collision.gameObject.CompareTag("Jumppad"))
        {
            rb2D.AddForce(Vector2.up * jumpPadForce, ForceMode2D.Impulse);
            Debug.Log("Jumppad");
        }
        else if (collision.gameObject.CompareTag("Hook"))
        {
            TakeDamage(4);
        }
        else if (collision.gameObject.CompareTag("saw"))
        {
            TakeDamage(1);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Region1"))
        {
            currentRegion = 1; // Set current region to 1
            Debug.Log("Horse entered Region 1.");
        }
        else if (other.CompareTag("Region2"))
        {
            currentRegion = 2; // Set current region to 2
            Debug.Log("Horse entered Region 2.");
        }
        else if (other.CompareTag("Region3"))
        {
            currentRegion = 3; // Set current region to 3
            Debug.Log("Horse entered Region 3.");
        }
        else if (other.CompareTag("Region4"))
        {
            currentRegion = 4; // Set current region to 4
            Debug.Log("Horse entered Region 4.");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Region1"))
        {
            currentRegion = 1; // Set current region to 1
            Debug.Log("Horse entered Region 1.");
        }
        else if (other.CompareTag("Region2"))
        {
            currentRegion = 2; // Set current region to 2
            Debug.Log("Horse entered Region 2.");
        }
        else if (other.CompareTag("Region3"))
        {
            currentRegion = 3; // Set current region to 3
            Debug.Log("Horse entered Region 3.");
        }
        else if (other.CompareTag("Region4"))
        {
            currentRegion = 4; // Set current region to 4
            Debug.Log("Horse entered Region 4.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Region1") || other.CompareTag("Region2") || other.CompareTag("Region3") || other.CompareTag("Region4"))
        {
            currentRegion = 0; // Reset to default region when exiting any region
            Debug.Log("Horse exited a region.");
        }
    }
}