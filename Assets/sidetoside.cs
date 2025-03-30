using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sidetoside : MonoBehaviour
{
    public float speed = 2f; // Speed of movement
    public float distance = 3f; // Distance to move from the starting position
    private Vector3 startPosition; // The starting position of the object
    private bool movingRight = true; // Direction of movement
    
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position; // Store the starting position
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object side to side
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

            // Check if the object has moved beyond the right limit
            if (transform.position.x >= startPosition.x + distance)
            {
                movingRight = false; // Change direction
            }
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;

            // Check if the object has moved beyond the left limit
            if (transform.position.x <= startPosition.x - distance)
            {
                movingRight = true; // Change direction
            }
        }
    }

}