using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoSkateVoiceManager : MonoBehaviour
{
    public AudioClip chooseCharacter;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = chooseCharacter;
    }

    public void PlayVoiceLine(AudioClip voiceLine)
    {
        audioSource.clip = voiceLine;
        audioSource.Play();
    }
}
