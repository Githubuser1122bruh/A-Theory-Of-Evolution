using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killinstant : MonoBehaviour
{
    public Health1Script playerHealth; // Reference to the player's health script
    public horsehealth horseHealth; // Reference to the horse's health script
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Onclick() 
    {
        playerHealth.TakeDamage(5); // Reduce player health by 5
        horseHealth.TakeDamage(4); // Reduce horse health by 3
    }
}
