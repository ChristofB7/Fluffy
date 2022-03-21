using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//Main idea is have a list of sounds, and it will play the sounds when needed


public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] AudioSource themeSource;
    [SerializeField] AudioSource RealthemeSource;
    [SerializeField] AudioClip themeClip;

    public static AudioManager instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        float volume = PlayerPrefs.GetFloat("volume", 0.5f);
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        RealthemeSource.Play();

    }

    public void playNext()
    {
        RealthemeSource.Stop();
        themeSource.Play();
        
    }


}
