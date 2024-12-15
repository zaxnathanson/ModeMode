using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] float pitchMin;
    [SerializeField] float pitchMax;
    [SerializeField] bool playOnAwake;
    private void Start()
    {
        if (playOnAwake)
        {
            PlayAudio();
        }
    }

    public void PlayAudio()
    {
        audioSource.pitch = Random.Range(pitchMin, pitchMax);
        audioSource.Play();
    }
    public void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.pitch = Random.Range(pitchMin, pitchMax);
        audioSource.Play();
    }
}
