using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private KeyCollection keyCollection;
    private bool isUnlocked = false;

    // Start is called before the first frame update
    void Start()
    {
        keyCollection = FindObjectOfType<KeyCollection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (keyCollection.UnlockedDoors && !isUnlocked)
        {
            UnlockDoor();
        }
    }

    private void UnlockDoor()
    {
        isUnlocked = true;
        GetComponent<BoxCollider2D>().enabled = false;
        Debug.Log("Door unlocked.");
        // Play door opening animation or sound
    }
}