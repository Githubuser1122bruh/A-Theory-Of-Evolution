using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionwithplayercarshower : MonoBehaviour
{
    public GameObject carPrefab; // Reference to the car prefab
    public Transform Player;
    public Transform Horse;
    public Transform playerTargetPosition; // Target position for the player
    public Transform horseTargetPosition; // Target position for the horse

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Horse"))
        {
            Debug.Log("Player or Horse reached the end of the level.");

            // Instantiate the car prefab at the position of this object
            Instantiate(carPrefab, transform.position, transform.rotation);

            // Move the player and horse to their respective target positions
            if (Player != null && playerTargetPosition != null)
            {
                Player.position = playerTargetPosition.position;
            }
            if (Horse != null && horseTargetPosition != null)
            {
                Horse.position = horseTargetPosition.position;
            }

            // Destroy this object
            Destroy(gameObject);
        }
    }
}