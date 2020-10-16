using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform breathPlace;
    public GameObject breath;
    public AudioClip death;

    private Transform objetivo;
    private Vector2 spawnPlace;
    private Animator anim;
    private bool attacking;
    private bool teleporting;
    private bool dead;
    private Enemigos stats;
    private AudioSource reproductor;
    private AudioTres reproductorExterno;
    void Start()
    {
        spawnPlace = transform.position;
        anim = GetComponent<Animator>();
        stats = GetComponent<Enemigos>();
        reproductor = GetComponent<AudioSource>();
        attacking = false;
        dead = false;
        reproductorExterno = GameObject.FindGameObjectWithTag("Reproductor3").GetComponent<AudioTres>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (stats.GetVidaActual() <= 0 && !dead)
        {
            StopAllCoroutines();
            dead = true;
            reproductor.clip = death;
            reproductor.Play();
        }
        GameObject objetivoX = GameObject.FindGameObjectWithTag("Player");
        if (objetivoX != null)
        {
            objetivo = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            if ((transform.position - objetivo.position).x < 0)
            {
                transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
            }
            else if ((transform.position - objetivo.position).x > 0)
            {
                transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            }

            if (Vector2.Distance(transform.position, objetivo.position) < 12 && Vector2.Distance(transform.position, objetivo.position) > 3 && !teleporting)
            {
                //print("distancia: " + Vector2.Distance(transform.position, objetivo.position));
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(objetivo.position.x, objetivo.position.y + 1), 2f * Time.deltaTime);
                
            }
            if (Vector2.Distance(transform.position, objetivo.position) < 4 && !attacking)
            {
                print("rango de ataque");
                attacking = true;
                StartCoroutine(FireBreath());
            }
            if (Vector2.Distance(transform.position, objetivo.position) >= 12 || Vector2.Distance(transform.position, spawnPlace) >= 20)
            {
                
                StartCoroutine(Teleport());
            }

        }
    }

    IEnumerator Teleport()
    {
        teleporting = true;
        anim.SetTrigger("Teleporting");
        yield return new WaitForSeconds(2.5f);
        transform.position = spawnPlace;
        anim.SetTrigger("Idle");
        teleporting = false;
    }

    IEnumerator FireBreath()
    {
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(.8f);
        reproductorExterno.PlayFireBreath();
        yield return new WaitForSeconds(.4f);
        Instantiate(breath, breathPlace.position, transform.rotation);
        yield return new WaitForSeconds(2.65f);
        attacking = false;
    }
}
