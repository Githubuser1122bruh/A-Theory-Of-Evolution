using UnityEngine;

public class TeleportToGround : MonoBehaviour
{
    public Transform teleportLocation; // The location to teleport the player to
    public string targetObjectName = "GroundTeleporter"; // The name of the specific object to trigger teleportation
    public Vector3 teleportOffset = new Vector3(0, 1, 0); // Offset to ensure the player is above the platform

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == targetObjectName)
        {
            Debug.Log("Collision detected with: " + collision.gameObject.name);
            transform.position = teleportLocation.position + teleportOffset; // Teleport the player with offset
        }
    }
}