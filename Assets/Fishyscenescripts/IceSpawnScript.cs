using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpawnScript : MonoBehaviour
{
    public GameObject iceChunkPrefab; // Prefab of the ice chunk to spawn
    public int numberOfChunks = 4; // Number of ice chunks to spawn
    public Transform spawnArea; // The transparent cube defining the spawn area
    public float minDistance = 2f; // Minimum distance between ice chunks

    private List<Vector3> spawnedPositions = new List<Vector3>();

    void Start()
    {
        SpawnIceChunks();
    }

    void SpawnIceChunks()
    {
        if (iceChunkPrefab == null || spawnArea == null)
        {
            Debug.LogError("Ice chunk prefab or spawn area is not assigned.");
            return;
        }

        for (int i = 0; i < numberOfChunks; i++)
        {
            SpawnNewIceChunk();
        }
    }

    public void SpawnNewIceChunk()
    {
        Vector3 randomPosition = GetRandomPositionWithinArea();
        GameObject iceChunk = Instantiate(iceChunkPrefab, randomPosition, Quaternion.identity);
        iceChunk.tag = "IceChunks";
        IceDrift iceDrift = iceChunk.AddComponent<IceDrift>(); // Add the floating behavior
        iceDrift.spawnArea = spawnArea; // Assign the spawn area to the IceDrift script
    }

    Vector3 GetRandomPositionWithinArea()
    {
        Vector3 randomPosition;
        int attempts = 0;
        do
        {
            Vector3 center = spawnArea.position;
            Vector3 size = spawnArea.localScale;

            float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
            float randomY = Random.Range(center.y - size.y / 2, center.y + size.y / 2);
            float randomZ = Random.Range(center.z - size.z / 2, center.z + size.z / 2);

            randomPosition = new Vector3(randomX, randomY, randomZ);
            attempts++;
        } while (!IsPositionValid(randomPosition) && attempts < 100);

        spawnedPositions.Add(randomPosition);
        return randomPosition;
    }

    bool IsPositionValid(Vector3 position)
    {
        foreach (Vector3 spawnedPosition in spawnedPositions)
        {
            if (Vector3.Distance(position, spawnedPosition) < minDistance)
            {
                return false;
            }
        }
        return true;
    }
}