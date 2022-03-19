using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider slider;

    public void Awake()
    {
        float volume = PlayerPrefs.GetFloat("volume", 0.5f);
        slider.value = volume;
    }
    public AudioMixer audioMixer;
    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
       
    }
    public void UpdateVolume()
    {
        float volume = PlayerPrefs.GetFloat("volume", 0.5f);
        slider.value = volume;
    }

}
