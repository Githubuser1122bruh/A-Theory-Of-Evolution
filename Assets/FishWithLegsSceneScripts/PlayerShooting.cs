using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public bool isShooting;
    public GameObject bulletPrefab;
    public Transform firePoint; // This is the point from where bullets are fired
    public float bulletSpeed = 20f;
    private Camera mainCamera;
    public BossAI Bossai;
    public float secondtoshoot = 1f;
    private float lastShotTime;

    // Start is called before the first frame update
    void Start()
    {
        isShooting = false;
        mainCamera = Camera.main;
        lastShotTime = -secondtoshoot; // Initialize to allow immediate shooting
    }

    // Update is called once per frame
    void Update()
    {
        if (isShooting)
        {
            if (Input.GetKeyDown(KeyCode.E) && Time.time >= lastShotTime + secondtoshoot)
            {
                Shoot();
                lastShotTime = Time.time;
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                return;
            }
        }
    }

    public void Shoot()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - firePoint.position.x, mousePosition.y - firePoint.position.y);
        direction.Normalize();

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;
    }
}