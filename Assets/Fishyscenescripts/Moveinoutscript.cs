using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveinoutscript : MonoBehaviour
{
    public Transform targetSprite;
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float speed = 1f;
    public float delay = 2f; // Delay between movements
    private bool movingIn = true;
    private float delayTimer = 0f;
    private SpriteRenderer spriteRenderer;
    private bool isFlipped = false;

    void Start()
    {
        if (targetSprite != null)
        {
            targetSprite.position = startPosition;
            spriteRenderer = targetSprite.GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        if (targetSprite != null)
        {
            if (delayTimer > 0)
            {
                delayTimer -= Time.deltaTime;
                return;
            }

            if (movingIn)
            {
                targetSprite.position = Vector3.MoveTowards(targetSprite.position, endPosition, speed * Time.deltaTime);
                if (Vector3.Distance(targetSprite.position, endPosition) < 0.01f)
                {
                    movingIn = false;
                    delayTimer = delay; // Set the delay timer
                    FlipSprite(); // Flip the sprite
                }
            }
            else
            {
                targetSprite.position = Vector3.MoveTowards(targetSprite.position, startPosition, speed * Time.deltaTime);
                if (Vector3.Distance(targetSprite.position, startPosition) < 0.01f)
                {
                    movingIn = true;
                    delayTimer = delay; // Set the delay timer
                    FlipSprite(); // Flip the sprite
                }
            }
        }
    }

    void FlipSprite()
    {
        if (spriteRenderer != null)
        {
            isFlipped = !isFlipped;
            spriteRenderer.flipX = isFlipped;
        }
    }
}