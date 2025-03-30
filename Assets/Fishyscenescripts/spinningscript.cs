using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningScript : MonoBehaviour
{
    public float diameter = 5f; // Diameter of the circle
    public float speed = 1f; // Speed of the circular movement

    private float radius;
    private float angle;
    private Vector3 centerPosition;

    void Start()
    {
        radius = diameter / 2f; // Calculate the radius from the diameter
        centerPosition = transform.position; // Store the initial position as the center of the circle
    }

    void Update()
    {
        // Calculate the new position
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        // Update the position of the object relative to the center position
        transform.position = new Vector3(centerPosition.x + x, centerPosition.y + y, centerPosition.z);

        // Increment the angle based on the speed
        angle += speed * Time.deltaTime;

        // Keep the angle within 0 to 2*PI range
        if (angle > Mathf.PI * 2)
        {
            angle -= Mathf.PI * 2;
        }
    }
}