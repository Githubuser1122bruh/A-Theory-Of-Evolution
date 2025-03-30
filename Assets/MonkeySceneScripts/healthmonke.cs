using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthmonke : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    public Image[] hearts; // Array of heart images
    public Sprite fullHeart; // Sprite for a full heart
    public Sprite emptyHeart; // Sprite for an empty heart

    public Transform playerTransform;
    public Transform checkpoint1; // Default checkpoint
    public Transform checkpoint2; // Checkpoint for region 2
    public Transform checkpoint3; // Checkpoint for region 3
    public Transform checkpoint4; // Checkpoint for region 4

    private int currentRegion = 0; // Tracks the current region (0 = default, 1 = region 1, 2 = region 2, 3 = region 3, 4 = region 4)

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHeartsUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't go below 0 or above maxHealth
        UpdateHeartsUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't go below 0 or above maxHealth
        UpdateHeartsUI();
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

            hearts[i].enabled = i < maxHealth;
        }
    }

    void Die()
    {
        Debug.Log("Player has died.");

        // Respawn at the appropriate checkpoint based on the current region
        switch (currentRegion)
        {
            case 1:
                playerTransform.position = checkpoint1.position;
                break;
            case 2:
                playerTransform.position = checkpoint2.position;
                break;
            case 3:
                playerTransform.position = checkpoint3.position;
                break;
            case 4:
                playerTransform.position = checkpoint4.position;
                break;
            default:
                playerTransform.position = checkpoint1.position; // Default checkpoint
                break;
        }

        currentHealth = maxHealth;
        UpdateHeartsUI();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AntNest"))
        {
            TakeDamage(1);
        }
        else if (collision.gameObject.CompareTag("BeeHive"))
        {
            TakeDamage(1);
        }
        else if (collision.gameObject.CompareTag("frog"))
        {
            TakeDamage(1);
        }
        else if (collision.gameObject.CompareTag("Rat"))
        {
            TakeDamage(1);
        }
        else if (collision.gameObject.CompareTag("void"))
        {
            TakeDamage(5);
        }
        else if (collision.gameObject.CompareTag("Health"))
        {
            Heal(1);
            Destroy(collision.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Region1"))
        {
            currentRegion = 1;
            Debug.Log("Entered Region 1");
        }
        else if (other.CompareTag("Region2"))
        {
            currentRegion = 2;
            Debug.Log("Entered Region 2");
        }
        else if (other.CompareTag("Region3"))
        {
            currentRegion = 3;
            Debug.Log("Entered Region 3");
        }
        else if (other.CompareTag("Region4"))
        {
            currentRegion = 4;
            Debug.Log("Entered Region 4");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Region1"))
        {
            currentRegion = 1;
            Debug.Log("Staying in Region 1");
        }
        else if (other.CompareTag("Region2"))
        {
            currentRegion = 2;
            Debug.Log("Staying in Region 2");
        }
        else if (other.CompareTag("Region3"))
        {
            currentRegion = 3;
            Debug.Log("Staying in Region 3");
        }
        else if (other.CompareTag("Region4"))
        {
            currentRegion = 4;
            Debug.Log("Staying in Region 4");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Region1") || other.CompareTag("Region2") || other.CompareTag("Region3") || other.CompareTag("Region4"))
        {
            currentRegion = 0; // Reset to default region when exiting any region
            Debug.Log("Exited a region. Resetting to default region.");
        }
    }
}