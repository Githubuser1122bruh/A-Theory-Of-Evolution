using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollection : MonoBehaviour
{
    public bool HasAllKeys = false;
    public bool HasYellow = false;
    public bool HasGreen = false;
    public bool HasOrange = false;
    public bool UnlockedDoors = false;

    public SharkChase sharkchase;
    public Transform resetplace;

    private GameObject orangeKey; // Reference to the Orange Key object
    private GameObject yellowKey; // Reference to the Yellow Key object
    private GameObject greenKey;  // Reference to the Green Key object

    void Update()
    {
        if (HasYellow && HasGreen && HasOrange)
        {
            HasAllKeys = true;
            Debug.Log("All keys collected.");
        }

        if (HasAllKeys)
        {
            UnlockedDoors = true;
            Debug.Log("Doors unlocked.");
        }
        else
        {
            UnlockedDoors = false;
        }

        // Make the collected keys follow the player
        if (HasOrange && orangeKey != null)
        {
            orangeKey.transform.position = transform.position;
        }
        if (HasYellow && yellowKey != null)
        {
            yellowKey.transform.position = transform.position;
        }
        if (HasGreen && greenKey != null)
        {
            greenKey.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("YellowKey"))
        {
            HasYellow = true;
            Debug.Log("Yellow key collected.");
            yellowKey = other.gameObject; // Store reference
            transform.position = resetplace.position;
        }
        else if (other.gameObject.CompareTag("GreenKey"))
        {
            HasGreen = true;
            Debug.Log("Green key collected.");
            greenKey = other.gameObject; // Store reference
            transform.position = resetplace.position;
        }
        else if (other.gameObject.CompareTag("OrangeKey"))
        {
            HasOrange = true;
            Debug.Log("Orange key collected.");
            orangeKey = other.gameObject; // Store reference

            // Stop the shark from chasing
            sharkchase.StopChase();
            transform.position = resetplace.position;
        }

        // Reset player's position after collecting any key
        
    }
}
