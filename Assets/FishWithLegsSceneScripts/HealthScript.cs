using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    float playerhealth = 5;
    public Transform playerTransform;
    public Transform checkpoint1;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public int maxHealth = 5;
    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHeartsUI();
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

    void UpdateHeartsUI() 
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i <currentHealth)
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
        playerTransform.position = checkpoint1.position;
        ResetHearts();
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.CompareTag("rock"))
        {
            Debug.Log("hit rock");
            TakeDamage(1);
        }
        else if (other.gameObject.CompareTag("bird"))
        {
            Debug.Log("player hit bird");
            TakeDamage(1);
        }
        else if (other.gameObject.CompareTag("void"))
        {
            Debug.Log("player entered void");
            TakeDamage(5);
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            Debug.Log("player hit boss");
            TakeDamage(1);
        }
    }
    void ResetHearts()
    {
        currentHealth = maxHealth; // Reset health to maximum
        UpdateHeartsUI(); // Update UI to show full hearts
    }
}

