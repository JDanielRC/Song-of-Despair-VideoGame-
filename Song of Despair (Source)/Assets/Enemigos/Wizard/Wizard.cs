using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform objetivo;
    private Vector2 posicionIni;
    private float fireballCD;
    private Animator anim;
    private AudioSource reproductor;
    private Enemigos estadisticas;
    private MovimientoParrot perico;
    private bool lookingRight;

    public GameObject fireball;
    public Transform referencia;
    public AudioClip audioFuego;
    public float platformRange;

    public 
    void Start()
    {
        posicionIni = transform.position;
        fireballCD = 3.4f;
        anim = GetComponent<Animator>();
        reproductor = GetComponent<AudioSource>();
        estadisticas = GetComponent<Enemigos>();
    }

    // Update is called once per frame
    void Update()
    {
        //implementacion bola fuego cuando esta a 8 unidades del jugador
        fireballCD -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        GameObject objetivoX = GameObject.FindGameObjectWithTag("Player");
        if (objetivoX != null)
        {
            objetivo = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            if (Vector2.Distance(transform.position, posicionIni) <= 10)  //sistema para seguir al jugador cierta distancia, y de-aggro si se aleja mucho de donde estaba
            {
                Vector2 direccion = (objetivo.position - transform.position).normalized * 120f;
                if (direccion.x >= 0)
                {
                    lookingRight = false;
                    Flip();
                }
                else
                {
                    lookingRight = true;
                    Flip();
                }
                if (Vector2.Distance(transform.position, objetivo.position) < 8) //detecta al jugador a cierta distancia

                {
                    if (Vector2.Distance(transform.position, objetivo.position) < 8 && fireballCD < 0 && estadisticas.GetVidaActual() > 0)
                    {
                        StartCoroutine(AttackPlayer());
                    }
                }

            }
        }
        

        if (estadisticas.GetVidaActual() <= 0)
        {
            StopAllCoroutines();
        }
        

    }
    IEnumerator AttackPlayer()
    {

        fireballCD = 3.4f;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(1.2f);
        reproductor.clip = audioFuego;
        reproductor.volume = .5f;
        reproductor.Play();
        Instantiate(fireball, referencia.position, fireball.transform.rotation);
        
        
    }

    void Flip()
    {
        if (lookingRight == true)
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            lookingRight = false;
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
            lookingRight = true;
        }

    }
}
