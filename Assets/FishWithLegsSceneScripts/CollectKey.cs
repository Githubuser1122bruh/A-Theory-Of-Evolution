using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectKey : MonoBehaviour
{
    public GameObject door;
    private Collider2D doorCollider;

    // Start is called before the first frame update
    void Start()
    {
        door.SetActive(true);
        doorCollider = door.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController.hasKey)
            {
                Debug.Log("Player has the key. Disabling door collider.");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            Debug.Log("Player collected the key.");
            playerController.hasKey = true;
            Destroy(door);
            Destroy(gameObject);
        }
    }
}