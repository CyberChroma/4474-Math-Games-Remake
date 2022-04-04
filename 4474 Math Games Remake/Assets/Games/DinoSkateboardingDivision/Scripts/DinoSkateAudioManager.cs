using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DinoSkateAudioManager : MonoBehaviour
{
    public AudioSource voiceManager;
    public AudioSource levelMusic;
    public AudioSource charSelectMusic;
    public Slider volumeSlider;
    public float pauseVolumeMultiplier;

    private float voiceMaxVolume;
    private float levelMusicMaxVolume;
    private float charSelectMusicMaxVolume;

    // Start is called before the first frame update
    void Start()
    {
        if (voiceManager) {
            voiceMaxVolume = voiceManager.volume;
        }
        levelMusicMaxVolume = levelMusic.volume;
        if (charSelectMusic) {
            charSelectMusicMaxVolume = charSelectMusic.volume;
        }
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1);
    }

    public void Pause()
    {
        voiceManager.Pause();
        levelMusic.volume = volumeSlider.value * levelMusicMaxVolume * pauseVolumeMultiplier;
        if (charSelectMusic) {
            charSelectMusic.volume = volumeSlider.value * charSelectMusicMaxVolume * pauseVolumeMultiplier;
        }
    }

    public void Resume()
    {
        voiceManager.UnPause();
        levelMusic.volume = volumeSlider.value * levelMusicMaxVolume;
        if (charSelectMusic) {
            charSelectMusic.volume = volumeSlider.value * charSelectMusicMaxVolume;
        }
    }

    public void VolumeChanged()
    {
        if (voiceManager) {
            voiceManager.volume = volumeSlider.value * voiceMaxVolume;
        }
        levelMusic.volume = volumeSlider.value * levelMusicMaxVolume * pauseVolumeMultiplier;
        if (charSelectMusic) {
            charSelectMusic.volume = volumeSlider.value * charSelectMusicMaxVolume * pauseVolumeMultiplier;
        }
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }
}
