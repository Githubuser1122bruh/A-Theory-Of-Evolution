using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 5;
    private bool isOnSafeGround = false; // Flag to check if the player is on safe ground
    private Coroutine healthCoroutine;
    private bool playerAlive = true;
    public Transform playerTransform;
    public Transform respawnPoint;
    public Vector3 respawnOffset = new Vector3(0, 1, 0); // Example offset

    private MovementScript movementScript; // Corrected reference to MovementScript

    void Start()
    {
        movementScript = FindObjectOfType<MovementScript>(); // Get the MovementScript instance
    }

    void Update()
    {
        if (playerHealth <= 0 && playerAlive)
        {
            playerHealth = 0; // Cap health at 0
            Debug.Log("Player has died!");
            playerAlive = false; // Set playerAlive to false
            HandlePlayerDeath();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            StopHealthDecrease(); // Stop health decrease when entering water
            Debug.Log("Player entered water");
        }
        else if (other.CompareTag("SafeGround")) // Use tags instead of name checks
        {
            isOnSafeGround = true;
            StopHealthDecrease(); // Stop health decrease when on safe ground
            Debug.Log("Player is on safe ground");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            CheckAndStartHealthDecrease(); // Start health decrease when exiting water (if not on safe ground)
            Debug.Log("Player exited water");
        }
        else if (other.CompareTag("SafeGround"))
        {
            isOnSafeGround = false;
            CheckAndStartHealthDecrease(); // Start health decrease when exiting safe ground (if not in water)
            Debug.Log("Player exited safe ground");
        }
    }

    void StopHealthDecrease()
    {
        if (healthCoroutine != null)
        {
            StopCoroutine(healthCoroutine);
            healthCoroutine = null;
        }
    }

    void CheckAndStartHealthDecrease()
    {
        // Use MovementScript.waterCounter to check if the player is still in water
        if (movementScript != null && movementScript.waterCounter <= 0 && !isOnSafeGround)
        {
            if (gameObject.activeInHierarchy)
            {
                if (healthCoroutine == null)
                {
                    healthCoroutine = StartCoroutine(DecreaseHealthOverTime());
                }
            }
            else
            {
                Debug.LogWarning("Cannot start coroutine because the player GameObject is inactive.");
            }
        }
        else
        {
            StopHealthDecrease(); // Ensures health doesn't drain incorrectly
        }
    }

    IEnumerator DecreaseHealthOverTime()
    {
        while (movementScript != null && movementScript.waterCounter <= 0 && !isOnSafeGround && playerHealth > 0)
        {
            playerHealth -= 1;
            Debug.Log("Player Health: " + playerHealth);
            yield return new WaitForSeconds(2);
        }
        healthCoroutine = null; // Reset coroutine reference when done
    }

    void HandlePlayerDeath()
    {
        Debug.Log("Respawning player at respawn point...");
        StartCoroutine(RespawnPlayer()); // Start the respawn process
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(2); // Wait for a short delay before respawning

        // Reset health and position
        playerHealth = 5;
        playerTransform.position = respawnPoint.position + respawnOffset;
        playerAlive = true;

        // Ensure the player GameObject is active
        gameObject.SetActive(true);

        // Check if health should decrease after respawning
        CheckAndStartHealthDecrease();
    }
}