using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killonimpact : MonoBehaviour
{
    public string doorTag = "Door"; // Tag to check for

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            Destroy(gameObject); // Destroy the boulder
        }
    }
}