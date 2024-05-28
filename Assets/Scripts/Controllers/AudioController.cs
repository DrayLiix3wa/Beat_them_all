using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    [Header("Source Audio")]
    public AudioSource audioSource;

    [Header("Clips Audios")]
    public AudioClip hitClip;
    public AudioClip bigHitClip;
    public AudioClip hurtClip;
    public AudioClip deathClip;
    public AudioClip blockClip;
    public AudioClip dashClip;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayHitSound()
    {
        audioSource.clip = hitClip;
        audioSource.Play();
    }

    public void PlayBigHitSound()
    {
        audioSource.clip = bigHitClip;
        audioSource.Play();
    }

    public void PlayHurtSound()
    {
        audioSource.clip = hurtClip;
        audioSource.Play();
    }

    public void PlayDeathSound()
    {
        audioSource.clip = deathClip;
        audioSource.Play();
    }

    public void PlayBlockSound()
    {
        audioSource.clip = blockClip;
        audioSource.Play();
    }

    public void PlayDashkSound()
    {
        audioSource.clip = dashClip;
        audioSource.Play();
    }
}

