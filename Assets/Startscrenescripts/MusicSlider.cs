using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetVolume);
        LoadVolume();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume(float volume)
    {
        float normalizedVolume = volume / 10f;
        if (normalizedVolume == 0)
        {
            audioMixer.SetFloat("MyExposedParam", -80f); // Mute the audio
        }
        else
        {
            audioMixer.SetFloat("MyExposedParam", Mathf.Log10(normalizedVolume) * 20);
        }
        PlayerPrefs.SetFloat("Volume", volume);
    }

    private void LoadVolume()
    {
        float volume = PlayerPrefs.GetFloat("Volume", 5f); // Default to middle value (5)
        volumeSlider.value = volume;
        SetVolume(volume);
    }
}