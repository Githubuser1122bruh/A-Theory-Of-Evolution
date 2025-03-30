using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawn : MonoBehaviour
{
    public GameObject barrelPrefab;
    public Transform startZone; // Where barrels spawn
    public Transform endZone;   // Where barrels get destroyed
    public float spawnRate = 2f; 
    public float barrelSpeed = 3f; 

    void Start()
    {
        StartCoroutine(SpawnBarrels());
    }

    IEnumerator SpawnBarrels()
    {
        while (true) // Infinite loop for spawning barrels
        {
            GameObject barrel = Instantiate(barrelPrefab, startZone.position, Quaternion.identity);
            BarrelMovement barrelMovement = barrel.AddComponent<BarrelMovement>(); // Add movement script
            barrelMovement.SetTarget(endZone.position, barrelSpeed);

            yield return new WaitForSeconds(spawnRate); // Wait before spawning the next barrel
        }
    }
}
