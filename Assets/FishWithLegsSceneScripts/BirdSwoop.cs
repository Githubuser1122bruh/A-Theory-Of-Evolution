using System.Collections;
using UnityEngine;

public class BirdSwoop : MonoBehaviour
{
    public Transform playerTransform;
    public float birdHeightAboveCamera = 5f; // Distance above the camera view
    public float swoopSpeed = 5f; // Speed of the swoop
    public float returnSpeed = 3f; // Speed to return to the original position
    public float swoopCooldownMin = 2f; // Minimum time between swoops
    public float swoopCooldownMax = 5f; // Maximum time between swoops
    public int damage = 1; // Damage dealt to the player on hit

    private Vector3 originalPosition; // Bird's resting position
    private bool isSwooping = false; // If the bird is currently swooping
    private bool isPlayerCrouching = false; // Track if the player is crouching
    private bool hasHitPlayer = false; // Track if the bird has hit the player during the swoop

    void Start()
    {
        // Start swooping at random intervals
        InvokeRepeating(nameof(Swoop), Random.Range(swoopCooldownMin, swoopCooldownMax), Random.Range(swoopCooldownMin, swoopCooldownMax));
        Debug.Log("BirdSwoop script started. Swoop initiated."); // Log script start
    }

    void Update()
    {
        if (!isSwooping) // Only follow player when not swooping
        {
            // Keep the bird always above the camera’s view, following the player
            Camera mainCam = Camera.main;
            float camTopY = mainCam.transform.position.y + (mainCam.orthographicSize + birdHeightAboveCamera);
            transform.position = new Vector3(playerTransform.position.x, camTopY, transform.position.z);
            originalPosition = transform.position; // Update original position
        }

        // Detect player crouching from the PlayerController script
        Movementscript playerController = playerTransform.GetComponent<Movementscript>();
        if (playerController != null)
        {
            isPlayerCrouching = playerController.isCrouching;
            
        }
        else
        {
            Debug.LogError("PlayerController script not found on playerTransform!"); // Log error if PlayerController is missing
        }
    }

    void Swoop()
    {
        if (!isSwooping)
        {
            Debug.Log("Starting swoop routine."); // Log swoop start
            StartCoroutine(SwoopRoutine());
        }
    }

    IEnumerator SwoopRoutine()
    {
        isSwooping = true;
        hasHitPlayer = false; // Reset hit flag at the start of the swoop
        Debug.Log("Bird is swooping."); // Log swooping state

        Vector3 swoopTarget = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);

        // Move down toward the player
        while (Vector3.Distance(transform.position, swoopTarget) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, swoopTarget, swoopSpeed * Time.deltaTime);

            // Rotate toward movement direction
            RotateTowards(swoopTarget, false); // Normal rotation when swooping down

            yield return null;
        }

        Debug.Log("Bird reached player position. Checking for crouch."); // Log reaching player

        // Check if the bird hit the player during the swoop
        if (hasHitPlayer && !isPlayerCrouching)
        {
            Debug.Log("Player is not crouching. Dealing damage."); // Log damage event
            // Deal damage to the player (assuming the player has a HealthScript)
            HealthScript playerHealth = playerTransform.GetComponent<HealthScript>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Pass the damage value here
            }
            else
            {
                Debug.LogError("HealthScript not found on playerTransform!"); // Log error if HealthScript is missing
            }
        }
        else
        {
            Debug.Log("Player is crouching or bird missed. No damage dealt."); // Log crouch avoidance or miss
        }

        // Simulate attack delay
        yield return new WaitForSeconds(0.5f);

        // Move back up
        while (Vector3.Distance(transform.position, originalPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, returnSpeed * Time.deltaTime);

            // Rotate towards movement direction, but reversed for going back up
            RotateTowards(originalPosition, true); // Reverse rotation when returning

            yield return null;
        }

        Debug.Log("Bird returned to original position."); // Log return to original position
        isSwooping = false; // Allow the bird to follow again
    }

    void RotateTowards(Vector3 targetPosition, bool isReturning)
    {
        Vector3 direction = targetPosition - transform.position; // Get direction vector
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Convert to degrees

        if (isReturning)
        {
            angle += 180f; // Reverse the rotation when going back up
        }

        transform.rotation = Quaternion.Euler(0, 0, angle); // Apply rotation
    }

    // Detect collision with the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Ensure the player has the "Player" tag
        {
            hasHitPlayer = true; // Set hit flag to true
            Debug.Log("Bird hit the player."); // Log collision
        }
    }
}