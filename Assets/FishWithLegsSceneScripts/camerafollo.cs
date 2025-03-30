using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target that the camera will follow
    public float smoothSpeed = 0.125f; // The speed with which the camera will follow the target
    public Vector3 offset; // The offset at which the camera will follow the target

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired position based on the target's position and the offset
            Vector3 desiredPosition = target.position + offset;
            
            // Maintain the camera's z position to avoid zooming in/out
            desiredPosition.z = transform.position.z;
            
            // Smoothly interpolate between the current position and the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            
            // Update the camera's position
            transform.position = smoothedPosition;
        }
    }
}