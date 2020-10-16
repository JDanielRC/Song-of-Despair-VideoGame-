using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acido : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    public AudioClip acido;
    private AudioSource reproductor;
    void Start()
    {
        reproductor = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        reproductor.clip = acido;
        StartCoroutine(Acidito());
    }

    // Update is called once per frame
    IEnumerator Acidito()
    {

        reproductor.volume = .1f;
        reproductor.Play();
        yield return new WaitForSeconds(.7f); //tiempo antes de que el acido suba
        print("rise acid");
        reproductor.volume = .55f;
        gameObject.tag = "AtaqueEnemigo"; //cambio tag para que haga el daño
        anim.SetTrigger("Rise");
        yield return new WaitForSeconds(3.4f); //tiempo en que vuelva a bajar
        gameObject.tag = "Untagged";
        anim.SetTrigger("Dissapear");
        reproductor.volume = .08f;
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
