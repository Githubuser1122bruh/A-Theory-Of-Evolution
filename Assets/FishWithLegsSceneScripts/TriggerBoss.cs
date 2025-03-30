using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoss : MonoBehaviour
{
    public BossAI boss;
    private PlayerShooting playerShooting;
    public HealthScript healthScript;
    public Transform duckspawn;
    public Transform duckpoint;

    void Start()
    {   
        healthScript = GameObject.FindWithTag("Player").GetComponent<HealthScript>();
        if (healthScript == null)
        {
            Debug.LogError("HealthScript component not found on Player!");
        }
        boss.running = false;
        playerShooting = GameObject.FindWithTag("Player").GetComponent<PlayerShooting>();

        if (playerShooting == null)
        {
            Debug.LogError("PlayerShooting component not found on Player!");
        }
        boss.canvas.SetActive(false);
    }

    void Update()
    {
        if (healthScript.currentHealth <= 0)
        {
            boss.transform.position = duckspawn.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered boss zone!");
            boss.running = true;
            playerShooting.isShooting = true;
            boss.canvas.SetActive(true);
            if (boss.bosshealth <= 0)
            {
                boss.canvas.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player exited boss zone!");
            boss.running = false;
            playerShooting.isShooting = false;
            boss.canvas.SetActive(false);
            boss.transform.position = duckpoint.position;
        }
    }
}
