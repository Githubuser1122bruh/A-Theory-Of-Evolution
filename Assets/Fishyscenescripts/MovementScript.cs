using UnityEngine;
using UnityEngine.UI;

public class MovementScript : MonoBehaviour
{
    public float swimSpeed = 5f;
    public float buoyancyForce = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    public Text waterCounterText; // Keep the water counter

    public int waterCounter { get; private set; } // Keep it public, but readonly outside this script

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UpdateWaterCounterUI();
    }

    void Update()
    {
        // Get player input
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        // Rotate the player to face the direction of movement
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }

    void FixedUpdate()
    {
        if (waterCounter > 0)
        {
            // Move the player
            rb.velocity = movement * swimSpeed;

            // Apply buoyancy if not moving vertically
            if (movement.y == 0)
            {
                rb.AddForce(Vector2.up * buoyancyForce, ForceMode2D.Force);
            }
        }
        else
        {
            // Reset velocity when out of water
            rb.velocity = Vector2.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            waterCounter++;
            UpdateWaterCounterUI();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            waterCounter--;
            UpdateWaterCounterUI();
        }
    }

    void UpdateWaterCounterUI()
    {
        if (waterCounterText != null)
        {
            waterCounterText.text = "Water: " + waterCounter;
        }
    }
}