using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    public AudioMixerGroup sfxAudioMixerGroup; // Reference to SFX Audio Mixer Group
    public AudioMixerGroup bgmAudioMixerGroup; // Reference to Background Music Audio Mixer Group
    public Slider sfxSlider; // Reference to SFX slider
    public Slider bgmSlider; // Reference to Background Music slider

    private const string SFXVolumeParam = "SFXVolume"; // Audio Mixer parameter name for SFX volume
    private const string BGMVolumeParam = "BGMVolume"; // Audio Mixer parameter name for Background Music volume

    void Start()
    {
        // Initialize slider values to current Audio Mixer values
        sfxSlider.value = sfxAudioMixerGroup.audioMixer.GetFloat(SFXVolumeParam, out float sfxVol) ? sfxVol : 0f;
        bgmSlider.value = bgmAudioMixerGroup.audioMixer.GetFloat(BGMVolumeParam, out float bgmVol) ? bgmVol : 0f;

        // Add listeners to slider changes
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
    }

    void SetSFXVolume(float volume)
    {
        sfxAudioMixerGroup.audioMixer.SetFloat(SFXVolumeParam, volume);
    }

    void SetBGMVolume(float volume)
    {
        bgmAudioMixerGroup.audioMixer.SetFloat(BGMVolumeParam, volume);
    }
}
