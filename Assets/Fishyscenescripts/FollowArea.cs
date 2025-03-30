using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowArea : MonoBehaviour
{
    public GameObject shark;
    public Transform playerTransform;
    private SharkChase sharkChase;

    // Start is called before the first frame update
    void Start()
    {
        if (shark != null)
        {
            sharkChase = shark.GetComponent<SharkChase>();
        }
    }

    // Detect when the player enters the area
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && sharkChase != null)
        {
            sharkChase.StartChase(playerTransform);
        }
    }

    // Detect when the player exits the area
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && sharkChase != null)
        {
            sharkChase.StopChase();
        }
    }
}