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
        voiceMaxVolume = voiceManager.volume;
        levelMusicMaxVolume = levelMusic.volume;
        charSelectMusicMaxVolume = charSelectMusic.volume;
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        voiceManager.Pause();
        levelMusic.volume = volumeSlider.value * levelMusicMaxVolume * pauseVolumeMultiplier;
        charSelectMusic.volume = volumeSlider.value * charSelectMusicMaxVolume * pauseVolumeMultiplier;
    }

    public void Resume()
    {
        voiceManager.UnPause();
        levelMusic.volume = volumeSlider.value * levelMusicMaxVolume;
        charSelectMusic.volume = volumeSlider.value * charSelectMusicMaxVolume;
    }

    public void VolumeChanged()
    {
        voiceManager.volume = volumeSlider.value * voiceMaxVolume;
        levelMusic.volume = volumeSlider.value * levelMusicMaxVolume * pauseVolumeMultiplier;
        charSelectMusic.volume = volumeSlider.value * charSelectMusicMaxVolume * pauseVolumeMultiplier;
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }
}
