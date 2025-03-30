using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openupbuddy : MonoBehaviour
{
    private Collider2D doorCollider;
    private humanmove playerScript;
    public Sprite newsprite; // Reference to the new sprite
    public Sprite oldsprite; // Reference to the old sprite
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        doorCollider = GameObject.FindGameObjectWithTag("Door").GetComponent<Collider2D>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<humanmove>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        doorCollider.isTrigger = false; // Ensure the door collider is non-trigger by default
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            doorCollider.isTrigger = true; // Open the door
            spriteRenderer.sprite = newsprite; // Change the door's color
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            doorCollider.isTrigger = false; // Close the door
            spriteRenderer.sprite = oldsprite; // Change the door's color
        }
    }
}