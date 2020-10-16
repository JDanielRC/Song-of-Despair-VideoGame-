using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDos : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public AudioClip playerDeath;
    public AudioClip experienceSound;
    public AudioClip openSound;

    private AudioSource reproductor;


    void Start()
    {
        reproductor = GetComponent<AudioSource>();
    }

    // Update is called once per frame


    public void PlayExpSound()
    {
        if (reproductor.isPlaying == true)
        {
            return;
        }
        reproductor.clip = experienceSound;
        reproductor.volume = .8f;
        reproductor.Play();
    }

    public void PlayPlayerDeath()
    {
        reproductor.clip = playerDeath;
        reproductor.volume = .5f;
        reproductor.Play();
    }

    public void PlayOpenSound()
    {
        reproductor.clip = openSound;
        reproductor.volume = 1f;
        reproductor.Play();
    }

}
