using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private AudioSource reproductor;
    public GameObject[] lootList;
    public AudioClip destruido;
    public int lootAmount;
    void Start()
    {
        anim = GetComponent<Animator>();
        reproductor = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WasHit()
    {
        anim.SetTrigger("Destroyed");
        reproductor.clip = destruido;
        reproductor.volume = .2f;
        reproductor.Play();
        StartCoroutine(DropLoot());
        
    }

    IEnumerator DropLoot()
    {
        yield return new WaitForSeconds(.09f);
        for (int i = 0; i < lootAmount; i++)
        {
            int index = Random.Range(0, lootList.Length);
            GameObject newItem = Instantiate(lootList[index], transform.position, transform.rotation);
            newItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(2f, 4.5f)), ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(.4f);
        Destroy(gameObject);
    }
}

