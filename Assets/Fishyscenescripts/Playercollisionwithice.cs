using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionWithIce : MonoBehaviour
{
    public Transform checkpoint; // The checkpoint to teleport the player to
    private IceSpawnScript iceSpawnScript;

    void Start()
    {
        iceSpawnScript = FindObjectOfType<IceSpawnScript>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("IceChunks"))
        {
            if (checkpoint != null)
            {
                transform.position = checkpoint.position;
            }
            else
            {
                Debug.LogError("Checkpoint is not assigned.");
            }

            // Destroy the ice chunk and spawn a new one
            Destroy(other.gameObject);
            if (iceSpawnScript != null)
            {
                iceSpawnScript.SpawnNewIceChunk();
            }
        }
    }
}