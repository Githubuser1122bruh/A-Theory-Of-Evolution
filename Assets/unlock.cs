using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlock : MonoBehaviour
{
    public healthcar healthcar; // Reference to the healthcar script
    public GameObject lockObject; // Reference to the lock GameObject
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    public Sprite unlockedSprite; // Sprite for the unlocked state
    public Sprite lockedSprite; // Sprite for the locked state
    public BoxCollider2D boxCollider; // Reference to the BoxCollider2D

    // Start is called before the first frame update
    void Start()
    {
        if (lockObject != null)
        {
            spriteRenderer = lockObject.GetComponent<SpriteRenderer>();
        }
        else
        {
            Debug.LogError("Lock object is not assigned in the Inspector.");
        }

        if (healthcar != null)
        {
            healthcar.hasknife = false; // Initialize hasknife to false
        }
        else
        {
            Debug.LogError("Healthcar reference is not assigned in the Inspector.");
        }
        boxCollider = GetComponent<BoxCollider2D>(); // Get the BoxCollider2D component
    }

    // Update is called once per frame
    void Update()
    {
        if (healthcar != null && spriteRenderer != null)
        {
            if (healthcar.hasknife)
            {
                spriteRenderer.sprite = unlockedSprite; // Set to unlocked sprite
                boxCollider.enabled = false; // Disable the BoxCollider2D
            }
            else
            {
                spriteRenderer.sprite = lockedSprite; // Set to locked sprite
                boxCollider.enabled = true; // Enable the BoxCollider2D
            }
        }
    }
}