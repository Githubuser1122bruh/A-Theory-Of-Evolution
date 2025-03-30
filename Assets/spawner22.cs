using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner22 : MonoBehaviour
{
    public GameObject boulderPrefab; // Reference to the boulder prefab
    public float spawnInterval = 2f; // Time interval between spawns
    public Vector2 pushForce = new Vector2(2f, 0f); // Force to push the boulder to the right
    public Collider2D collider2D;
    private float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        collider2D.isTrigger = true;
        spawnTimer = spawnInterval; // Initialize the spawn timer
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnBoulder();
            spawnTimer = spawnInterval; // Reset the spawn timer
        }
    }

    void SpawnBoulder()
    {
        // Instantiate the boulder prefab at the spawner's position
        GameObject boulder = Instantiate(boulderPrefab, transform.position, Quaternion.identity);

        // Ensure the boulder is at the correct Z-position
        boulder.transform.position = new Vector3(boulder.transform.position.x, boulder.transform.position.y, 0);

        // Apply a slight push to the right
        Rigidbody2D rb = boulder.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(pushForce, ForceMode2D.Impulse);
        }

        // Ensure the boulder is on the correct sorting layer
        SpriteRenderer sr = boulder.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sortingLayerName = "Default"; // Ensure it's on the Default sorting layer
            sr.sortingOrder = 1; // Ensure it's in front of the background
        }

        // Debug the boulder's position
        Debug.Log($"Boulder spawned at: {boulder.transform.position}");
    }
}