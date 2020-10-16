using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTres : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip levelUp;
    public AudioClip shutDoor;
    public AudioClip openDoor;
    public AudioClip parrotBook;
    public AudioClip equipItem;
    public AudioClip openInventory;
    public AudioClip fireBreath;
    public AudioClip spawnPhaseLaugh;
    public AudioClip secondPhaseYouu;
    public AudioClip thirdPhaseEnough;
    public AudioClip finalPhaseRoar;
    private AudioSource reproductor;
    void Start()
    {
        reproductor = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void PlayLevelUp()
    {
        reproductor.clip = levelUp;
        reproductor.volume = .5f;
        reproductor.Play();
    }

    public void PlayShutDoor()
    {
        reproductor.clip = shutDoor;
        reproductor.volume = 1;
        reproductor.Play();
    }

    public void PlayOpenDoor()
    {
        reproductor.clip = openDoor;
        reproductor.volume = 1;
        reproductor.Play();
    }

    public void PlayParrotBook()
    {
        reproductor.clip = parrotBook;
        reproductor.volume = 1;
        reproductor.Play();
    }

    public void PlayEquipItem()
    {
        reproductor.clip = equipItem;
        reproductor.volume = 1;
        reproductor.Play();
    }

    public void PlayOpenInventory()
    {
        reproductor.clip = openInventory;
        reproductor.volume = 1;
        reproductor.Play();
    }

    public void PlayFireBreath()
    {
        reproductor.clip = fireBreath;
        reproductor.volume = .6f;
        reproductor.Play();
    }

    

    public void PlaySpawnPhaseLaugh()
    {
        reproductor.clip = spawnPhaseLaugh;
        reproductor.volume = 1;
        reproductor.Play();
    }

    public void PlaySecondPhaseYouu()
    {
        reproductor.clip = secondPhaseYouu;
        reproductor.volume = 1;
        reproductor.Play();
    }

    public void PlayThirdPhaseEnough()
    {
        reproductor.clip = thirdPhaseEnough;
        reproductor.volume = 1;
        reproductor.Play();
    }

    public void PlayFinalPhaseRoar()
    {
        reproductor.clip = finalPhaseRoar;
        reproductor.volume = 1;
        reproductor.Play();
    }
}
