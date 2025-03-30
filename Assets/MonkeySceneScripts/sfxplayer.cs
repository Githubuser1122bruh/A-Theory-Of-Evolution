using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxplayer : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip sfx1, sfx2;
    public PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void PlayMovementSFX()
    {
        if (!audiosource.isPlaying || audiosource.clip != sfx1)
        {
            audiosource.clip = sfx1;
            audiosource.Play();
        }
    }

    public void PlayClimbingSFX()
    {
        if (!audiosource.isPlaying || audiosource.clip != sfx2)
        {
            audiosource.clip = sfx2;
            audiosource.Play();
        }
    }

    public void StopMovementSFX()
    {
        if (audiosource.isPlaying)
        {
            audiosource.Stop();
        }
    }
}