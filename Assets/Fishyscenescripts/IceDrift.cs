using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDrift : MonoBehaviour
{
    public float buoyancyForce = 2f;
    public float floatSpeed = 0.5f;
    public float floatRange = 0.5f;
    public Transform spawnArea; // The spawn area to keep the ice chunks within

    private Rigidbody2D rb;
    private Vector3 initialPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = 0; // Disable gravity for floating behavior
        rb.isKinematic = false; // Set Rigidbody2D to non-kinematic for physics interactions
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Prevent rotation
        initialPosition = transform.position;
    }

    void Update()
    {
        // Apply small random movement to simulate floating
        float offsetX = Mathf.Sin(Time.time * floatSpeed) * floatRange;
        float offsetY = Mathf.Cos(Time.time * floatSpeed) * floatRange;
        rb.MovePosition(initialPosition + new Vector3(offsetX, offsetY, 0));

        // Ensure the ice chunk stays within the spawn area
        KeepWithinSpawnArea();
    }

    void KeepWithinSpawnArea()
    {
        Vector3 center = spawnArea.position;
        Vector3 size = spawnArea.localScale;

        Vector3 position = transform.position;

        if (position.x < center.x - size.x / 2)
        {
            position.x = center.x - size.x / 2;
        }
        else if (position.x > center.x + size.x / 2)
        {
            position.x = center.x + size.x / 2;
        }

        if (position.y < center.y - size.y / 2)
        {
            position.y = center.y - size.y / 2;
        }
        else if (position.y > center.y + size.y / 2)
        {
            position.y = center.y + size.y / 2;
        }

        transform.position = position;
    }
}