using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : Singleton<SoundManager>
{

    AudioSource audioSource;

    public AudioClip buttonClickSound;
    public AudioClip ballSound;
    public AudioClip blockHitSound;
    public AudioClip blockDieSound;
    public AudioClip bombSound;
    public AudioClip teleportionSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ButtonClickSound()
    {
        audioSource.PlayOneShot(buttonClickSound);
    }
    public void BallSound()
    {
        audioSource.PlayOneShot(ballSound);
    }
    public void BlockHitSound()
    {
        audioSource.PlayOneShot(blockHitSound);
    }
    public void BlockDieSound()
    {
        audioSource.PlayOneShot(blockDieSound);
    }
}
