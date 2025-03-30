using UnityEngine;

public class BubbleEmitterController : MonoBehaviour
{
    public ParticleSystem bubbleSystem;
    public Rigidbody2D playerRb; // Reference to player's Rigidbody2D

    void Start()
    {
        bubbleSystem = GetComponent<ParticleSystem>();
        playerRb = GetComponentInParent<Rigidbody2D>(); // Get player's Rigidbody2D
    }

    void Update()
    {
        if (playerRb != null)
        {
            // Check if the player is moving
            if (playerRb.velocity.magnitude > 0.1f)
            {
                if (!bubbleSystem.isEmitting)
                    bubbleSystem.Play(); // Start emitting bubbles
            }
            else
            {
                if (bubbleSystem.isEmitting)
                    bubbleSystem.Stop(); // Stop emitting bubbles
            }
        }
    }
}
