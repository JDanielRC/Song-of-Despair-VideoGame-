using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esqueleto : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform objetivo;
    private Vector2 inicio;
    private bool spawned;
    private bool executed;
    private bool walking;
    private Animator anim;
    public AudioClip spawn;
    public AudioClip walk;
    private AudioSource reproductor;

    void Start()
    {
        reproductor = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        inicio = transform.position;
        spawned = false;
        walking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        GameObject objetivoX = GameObject.FindGameObjectWithTag("Player");
        if (objetivoX != null)
        {
            objetivo = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            if ((transform.position - objetivo.position).x < 0)
            {
                transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
            } else if ((transform.position - objetivo.position).x > 0)
            {
                transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            }
            if (Vector2.Distance(transform.position, objetivo.position) < 10)
            {
                if (!spawned && !executed)
                {
                    reproductor.clip = spawn;
                    reproductor.volume = 1;
                    reproductor.Play();
                    StartCoroutine(Spawned());
                }

            }
            if (Vector2.Distance(transform.position, objetivo.position) < 15 && spawned && executed)
            {
                StartCoroutine(FollowPlayer());
                if (!walking)
                {
                    reproductor.clip = walk;
                    reproductor.volume = .4f;
                    reproductor.loop = true;
                    reproductor.Play();
                    anim.SetTrigger("Walking");
                    walking = true;
                }
            }
            else if (Vector2.Distance(transform.position, objetivo.position) >= 15 && spawned && executed)
            {
                StartCoroutine(FollowInicio());
                anim.SetTrigger("NotWalking");
                reproductor.Stop();
                walking = false;
            }

        }
        
        
    }

    IEnumerator Spawned()
    {
        spawned = true;
        anim.SetTrigger("Spawn");
        yield return new WaitForSeconds(1.3f);
        executed = true;
    }

    IEnumerator FollowPlayer()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, objetivo.position, 2.05f * Time.deltaTime);
        yield return null;
    }

    IEnumerator FollowInicio()
    {
        yield return new WaitForSeconds(.4f);
        anim.SetBool("Walking", true);
        transform.position = Vector2.MoveTowards(transform.position, inicio, 1.2f * Time.deltaTime);
        if (Vector2.Distance(transform.position, inicio) < .1)
        {
            anim.SetTrigger("idle");
            walking = false;
        }
    }
}
