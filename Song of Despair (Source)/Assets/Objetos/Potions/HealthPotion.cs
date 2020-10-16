using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    // Start is called before the first frame update

    private PlayerController jugador;
    private MovimientoParrot jugadorPerico;
    private Animator anim;
    public AudioClip healSound;
    private AudioSource reproductor;
    private CambioForma manager;
    void Start()
    {
        anim = GetComponent<Animator>();
        reproductor = GetComponent<AudioSource>();
        manager = GameObject.FindGameObjectWithTag("ShapeController").GetComponent<CambioForma>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && manager.GetCurrentForm() == 1)
        {
            jugador = collision.GetComponent<PlayerController>();
            StartCoroutine(HealPlayer());
        } else if (collision.tag == "Player" && manager.GetCurrentForm() == 2)
        {
            jugadorPerico = collision.GetComponent<MovimientoParrot>();
            StartCoroutine(HealPlayerPerico());
        }
    }

    IEnumerator HealPlayer()
    {
        jugador.Heal((int) (jugador.GetVidaMaxima() * .25f + 20));
        reproductor.clip = healSound;
        reproductor.volume = .35f;
        reproductor.Play();
        anim.SetTrigger("Picked");
        yield return new WaitForSeconds(1.25f);
        Destroy(gameObject);
    }

    IEnumerator HealPlayerPerico()
    {
        jugadorPerico.Heal(10);
        reproductor.clip = healSound;
        reproductor.volume = .35f;
        reproductor.Play();
        anim.SetTrigger("Picked");
        yield return new WaitForSeconds(1.25f);
        Destroy(gameObject);
    }
}
