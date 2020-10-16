using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaFondo : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip cementerio;
    public AudioClip ciudad;
    public AudioClip bosque;
    public AudioClip cavernas;
    public AudioClip cavernas2;
    public AudioClip castillo;
    public AudioClip preBoss;
    public AudioClip firstBoss;
    private bool firstBossStarted;
    public AudioClip endingBoss;
    public AudioClip muerte;
    public AudioClip casa;
    public AudioClip battle;
    public AudioClip preEndingBoss;

    private AudioSource reproductor;
    void Start()
    {
        
        reproductor = GetComponent<AudioSource>();
        reproductor.loop = true;
        reproductor.clip = casa;
        reproductor.volume = .1f;
        firstBossStarted = false;
        reproductor.Play();
    }

    // Update is called once per frame

    public void PlayCasa()
    {
        reproductor.clip = casa;
        reproductor.volume = .1f;
        reproductor.Play();
    }

    public void PlayBattle()
    {
        reproductor.clip = battle;
        reproductor.volume = .25f;
        reproductor.Play();
    }

    public void PlayCementerio()
    {
        reproductor.clip = cementerio;
        reproductor.volume = .55f;
        reproductor.Play();
    }

    public void PlayCiudad()
    {
        reproductor.clip = ciudad;
        reproductor.volume = .55f;
        reproductor.Play();
    }

    public void PlayBosque()
    {
        reproductor.clip = bosque;
        reproductor.volume = .5f;
        reproductor.Play();
    }

    public void PlayCavernas()
    {
        reproductor.clip = cavernas;
        reproductor.volume = .3f;
        reproductor.Play();
    }

    public void PlayCavernas2()
    {
        reproductor.clip = cavernas2;
        reproductor.volume = .65f;
        reproductor.Play();
    }

    public void PlayCastillo()
    {
        reproductor.clip = castillo;
        reproductor.volume = .5f;
        reproductor.Play();
    }

    public void PlayPreBoss()
    {
        reproductor.clip = preBoss;
        reproductor.volume = .5f;
        reproductor.Play();
    }

    public void PlayFirstBoss()
    {
        if (!firstBossStarted)
        {
            reproductor.clip = firstBoss;
            reproductor.volume = .35f;
            reproductor.Play();
            firstBossStarted = true;
        }
    }

    public void PlayPreEndingBoss()
    {
        reproductor.clip = preEndingBoss;
        reproductor.volume = .55f;
        reproductor.Play();
    }
    public void PlayEndingBoss()
    {
        reproductor.Stop();
        reproductor.clip = endingBoss;
        reproductor.volume = .55f;
        reproductor.Play();
    }

    public void PlayMuerte()
    {
        reproductor.loop = false;
        reproductor.clip = muerte;
        reproductor.volume = .7f;
        reproductor.Play();
    }
}
