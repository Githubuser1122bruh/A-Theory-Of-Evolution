using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateYcollectiones : MonoBehaviour
{
    public float rotationspeed = 20f;
    public bool hascollectedplayer = false;
    public bool hascollectedhorse = false;
    // Update is called once per frame
    void Update()
    {
        // Rotate the object around the Z-axis
        transform.Rotate(0, 0, rotationspeed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collected the object.");
            Destroy(gameObject); // Destroy the object when collected
            hascollectedplayer = true;
        }
        else if (other.gameObject.CompareTag("Horse"))
        {
            Debug.Log("Horse collected the object.");
            Destroy(gameObject); // Destroy the object when collected
            hascollectedhorse = true;
        }
        else if (other.gameObject.CompareTag("Collectible"))
        {
            Debug.Log("Collectible collected the object idk why.");
            Destroy(gameObject); // Destroy the object when collected
        }
    }
}