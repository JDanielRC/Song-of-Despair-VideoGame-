using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageFire : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private AudioSource reproductor;
    public AudioClip fuego;
    void Start()
    {
        anim = GetComponent<Animator>();
        reproductor = GetComponent<AudioSource>();
        StartCoroutine(Fueguito());
    }

    // Update is called once per frame
    IEnumerator Fueguito()
    {
        //animación por default 
        yield return new WaitForSeconds(1f); //animacion que genera un circulo, advirtiendo que el fuego va a aparecer ahi
        reproductor.clip = fuego;
        reproductor.Play();
        yield return new WaitForSeconds(.2f);
        gameObject.tag = "AtaqueEnemigo"; //cambio tag para que haga el daño
        anim.SetTrigger("Burning"); //inicio la animacion del verdadero fuego
        yield return new WaitForSeconds(2); //dura dos segundos activo
        Destroy(gameObject); //y desaparece
    }
}
