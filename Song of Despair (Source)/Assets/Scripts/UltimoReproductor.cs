using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimoReproductor : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip summoning;
    private AudioSource reproductor;
    void Start()
    {
        reproductor = GetComponent<AudioSource>();
    }

    public void PlaySummoningSound()
    {
        if (reproductor.isPlaying == true)
        {
            return;
        }
        reproductor.clip = summoning;
        reproductor.volume = .8f;
        reproductor.Play();
    }
}
