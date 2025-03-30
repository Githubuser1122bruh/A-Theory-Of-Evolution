using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotaremovemurder : MonoBehaviour
{
    public float rotationspeed = 20f;
    public float movespeed = 7f;
    public Transform starttransform;
    public Transform endtransform;
    public Health1Script health1Script;
    public horsehealth horsehealth;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = starttransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around the Z-axis (spinning in place)
        transform.Rotate(0, 0, rotationspeed * Time.deltaTime);

        // Move the object from start to end
        transform.position = Vector3.MoveTowards(transform.position, endtransform.position, movespeed * Time.deltaTime);

        // Reset position to start when reaching the end
        if (transform.position == endtransform.position)
        {
            transform.position = starttransform.position;
        }
    }
    void onCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with the rotating object.");
            health1Script.TakeDamage(1);
        }
        if (other.gameObject.CompareTag("Horse"))
        {
            horsehealth.TakeDamage(1);
            Debug.Log("Horse collided with the rotating object.");
        }

    }
}