using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClownAnglesAudioManager : MonoBehaviour
{
    //public AudioSource voiceManager;
    public AudioSource levelMusic;
    public Slider volumeSlider;
    public float pauseVolumeMultiplier;

    private float voiceMaxVolume;
    private float levelMusicMaxVolume;

    // Start is called before the first frame update
    void Start()
    {
        //voiceMaxVolume = voiceManager.volume;
        levelMusicMaxVolume = levelMusic.volume;
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1);
    }

    public void Pause()
    {
        //voiceManager.Pause();
        levelMusic.volume = volumeSlider.value * levelMusicMaxVolume * pauseVolumeMultiplier;
    }

    public void Resume()
    {
        //voiceManager.UnPause();
        levelMusic.volume = volumeSlider.value * levelMusicMaxVolume;
    }

    public void VolumeChanged()
    {
        //voiceManager.volume = volumeSlider.value * voiceMaxVolume;
        levelMusic.volume = volumeSlider.value * levelMusicMaxVolume * pauseVolumeMultiplier;
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }
}
