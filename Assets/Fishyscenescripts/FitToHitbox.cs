using UnityEngine;
using UnityEngine.SceneManagement;

public class FitToHitbox : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public SpriteRenderer spriteRenderer;
    public Transform checkpoint;
    public Transform playerTransform;

    // Manually set the desired size and offset
    public Vector2 colliderSize = new Vector2(1, 1);
    public Vector2 colliderOffset = new Vector2(0, 0);

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set the collider size and offset manually
        boxCollider.size = colliderSize;
        boxCollider.offset = colliderOffset;
    }

    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision detected with " + other.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collided, going back to main menu.");
            playerTransform.position = checkpoint.position;
        }
    }
}