using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;
    [SerializeField] List<AudioSource> sfxAudioSources;
    [SerializeField] List<AudioSource> backgroundMusicAudioSources;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            // Find the object with audio sources in the Game scene
            GameObject gameAudioObject = GameObject.Find("Audio");
            if (gameAudioObject != null)
            {
                // Get the audio sources from the object
                AudioSource[] audioSources = gameAudioObject.GetComponentsInChildren<AudioSource>();

                // Add the audio sources to the lists in the Sound script
                foreach (AudioSource audioSource in audioSources)
                {
                    if (audioSource.tag == "SFX")
                    {
                        sfxAudioSources.Add(audioSource);
                    }
                    else if (audioSource.tag == "BackgroundMusic")
                    {
                        backgroundMusicAudioSources.Add(audioSource);
                    }
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }

        if (!PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("sfxVolume", 1);
            Load();
        }

        else
        {
            Load();
        }
    }

    // Update is called once per frame
    public void ChangeMusicVolume()
    {
        AudioListener.volume = musicVolumeSlider.value;
        foreach (AudioSource backgroundMusicAudioSource in backgroundMusicAudioSources)
        {
            backgroundMusicAudioSource.volume = musicVolumeSlider.value;
        }
        SaveMusicVolume();
    }

    public void ChangeSFXVolume()
    {
        foreach (AudioSource sfxAudioSource in sfxAudioSources)
        {
            sfxAudioSource.volume = sfxVolumeSlider.value;
        }
        SaveSFXVolume();
    }

    private void Load()
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        foreach (AudioSource backgroundMusicAudioSource in backgroundMusicAudioSources)
        {
            backgroundMusicAudioSource.volume = musicVolumeSlider.value;
        }
        foreach (AudioSource sfxAudioSource in sfxAudioSources)
        {
            sfxAudioSource.volume = sfxVolumeSlider.value;
        }
    }

    private void SaveMusicVolume()
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolumeSlider.value);
    }

    private void SaveSFXVolume()
    {
        PlayerPrefs.SetFloat("sfxVolume", sfxVolumeSlider.value);
    }
}
