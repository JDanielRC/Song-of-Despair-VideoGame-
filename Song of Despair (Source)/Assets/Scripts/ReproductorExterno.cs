using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReproductorExterno : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip healSound;
    public AudioClip pickupSound;
    public AudioClip openSound;
    public AudioClip damageSound;
    public AudioClip attackSound;
    public AudioClip openChestSound;
    private AudioSource reproductor;
    void Start()
    {
        reproductor = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayOpenChestSound ()
    {
        reproductor.clip = openChestSound;
        reproductor.volume = .8f;
        reproductor.Play();
    }

    public void PlayHealSound()
    {
        reproductor.clip = healSound;
        reproductor.volume = .4f;
        reproductor.Play();
    }
    public void PlayPickupSound()
    {
        reproductor.clip = pickupSound;
        reproductor.volume = .8f;
        reproductor.Play();
    }

    public void PlayOpenSound()
    {
        reproductor.clip = openSound;
        reproductor.volume = 1;
        reproductor.Play();
    }

    public void PlayHurtSound()
    {
        reproductor.clip = damageSound;
        reproductor.volume = .8f;
        reproductor.Play();
    }

    public void PlayAttackSound()
    {
        reproductor.clip = attackSound;
        reproductor.volume = 1;
        reproductor.Play();
    }
}
