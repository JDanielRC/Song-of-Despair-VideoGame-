using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] spawnPlaces;
    public GameObject enemigo;
    public AudioClip surprise;
    private bool executed;
    private GameObject clone;
    private AudioSource reproductor;
    void Start()
    {
        reproductor = GetComponent<AudioSource>();
        executed = false;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !executed)
        {
            reproductor.clip = surprise;
            reproductor.Play();
            StartCoroutine(SpawnEnemies());

        }
    }


    IEnumerator SpawnEnemies()
    {
        executed = true;
        for (int i = 0; i < spawnPlaces.Length; i++)
        {
            clone = Instantiate(enemigo, spawnPlaces[i].position, spawnPlaces[i].rotation);

            Destroy(clone, 30);
        }
        yield return null;
    }
}
