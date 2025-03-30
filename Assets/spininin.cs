using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spininin : MonoBehaviour
{
    public float radius = 5f; // The radius of the rotation
    public float rotationSpeed = 50f; // Speed of rotation (degrees per second)

    private float angle = 0f; // Current angle of rotation
    private Vector3 centerPosition; // The center point of the rotation

    // Start is called before the first frame update
    void Start()
    {
        // Set the center position to the object's initial position
        centerPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Increment the angle based on rotation speed and time
        angle += rotationSpeed * Time.deltaTime;

        // Convert the angle to radians
        float angleInRadians = angle * Mathf.Deg2Rad;

        // Calculate the new position based on the angle and radius
        float x = centerPosition.x + Mathf.Cos(angleInRadians) * radius;
        float y = centerPosition.y + Mathf.Sin(angleInRadians) * radius;

        // Update the position of the object
        transform.position = new Vector3(x, y, transform.position.z);

        // Calculate the tangent angle (direction of movement)
        float tangentAngle = Mathf.Atan2(Mathf.Sin(angleInRadians), Mathf.Cos(angleInRadians)) * Mathf.Rad2Deg;

        // Update the Z-axis rotation to face the direction of movement
        transform.rotation = Quaternion.Euler(0f, 0f, tangentAngle);
    }
}