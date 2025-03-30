using UnityEngine;

public class killplayerwithlavascript : MonoBehaviour
{
    public Transform checkpoint;
    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Correct method signature for detecting 2D collisions
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit the spikes!");
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerTransform.position = checkpoint.position;
            }
        }
    }
}