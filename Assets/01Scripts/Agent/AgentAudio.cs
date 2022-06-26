using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AgentAudio))]

public class AgentAudio : MonoBehaviour
{
    public AudioClip attackClip;
    public AudioClip walkClip;

    private AudioSource audioSource;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayWalkAudio()
    {
        if (walkClip == null)
            return;
        if (audioSource.isPlaying)
            return;
        audioSource.volume = 0.2f;
        audioSource.clip = walkClip;
        audioSource.Play();
    }

    public void PlayAttackSound()
    {
        if (attackClip == null)
            return;
        audioSource.volume = 1f;
        audioSource.clip = attackClip;
        audioSource.Play();
    }

    public void PlayClipSound(AudioClip clip)
    {
        audioSource.volume = 1f;
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void StopAudio()
    {
        audioSource?.Stop();
    }

    public void SetAudioVolume(float value)
    {
        audioSource.volume = value;
    }
}
