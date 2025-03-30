using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootcrap : MonoBehaviour
{
    public GameObject projectilePrefab; // The projectile to shoot
    public Transform shootPoint; // The point from which the projectile is fired
    public float shootInterval = 2f; // Time between shots
    public float projectileSpeed = 10f; // Speed of the projectile
    public float knockbackForce = 15f; // Knockback force applied to the player

    private Transform player; // Reference to the player's transform
    private float shootTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player by tag
        shootTimer = shootInterval;
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0f)
        {
            ShootProjectile();
            shootTimer = shootInterval;
        }
    }

    void ShootProjectile()
    {
        if (player == null) return;

        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        // Calculate the direction to the player
        Vector2 direction = (player.position - shootPoint.position).normalized;

        // Apply velocity to the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * projectileSpeed;
        }

        // Destroy the projectile after a certain time to avoid clutter
        Destroy(projectile, 3f);
    }
}