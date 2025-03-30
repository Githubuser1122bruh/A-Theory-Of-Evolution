using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class murdervoid : MonoBehaviour
{
    public Health1Script health1;
    public horsehealth horsehealth;

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
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with the rotating object.");
            health1.TakeDamage(5);
        }
        else if (other.gameObject.CompareTag("Horse"))
        {
            Debug.Log("Horse collided with the rotating object.");
            horsehealth.TakeDamage(5);
        }
    }
}