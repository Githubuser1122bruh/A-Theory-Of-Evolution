using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterHorse : MonoBehaviour
{
    public GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        wall.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player reached the teleporter.");
            wall.SetActive(false); // Open the door
        }
    }
}
