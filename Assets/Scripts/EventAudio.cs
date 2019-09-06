using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAudio : MonoBehaviour
{

    public AudioClip[] AudioClips;

    private AudioSource AudioSource;

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int index)
    {
        AudioSource.clip = AudioClips[index];
        AudioSource.Play();
    }
}

