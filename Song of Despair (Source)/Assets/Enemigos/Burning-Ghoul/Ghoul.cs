using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : MonoBehaviour
{
    // Start is called before the first frame update

    private Transform objetivo;
    private Animator anim;
    public Transform[] path;
    private int actual;
    public float rango = 0.8f;
    public bool lookingLeft;
    public bool lookingRight;
    private bool exploding = false;
    private bool isWalkingSound;
    private Enemigos stats;
    private Rigidbody2D rb2D;
    public LayerMask jugador;
    private CambioForma formaActual;
    private bool dead;

    public AudioClip explodeSound;
    public AudioClip tssSound;
    public AudioClip walking;
    public AudioClip dying;
    private AudioSource reproductor;
    void Start()
    {
        reproductor = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        actual = 0;
        dead = false;
        stats = GetComponent<Enemigos>();
        rb2D = GetComponent<Rigidbody2D>();
        formaActual = GameObject.FindGameObjectWithTag("ShapeController").GetComponent<CambioForma>();
        isWalkingSound = false;
        StartCoroutine(ChecarSiLlegue());
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.GetVidaActual() <= 0 && !exploding)
        {
            rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            
        }
        else if (!exploding)
        {
            transform.position = Vector2.MoveTowards(transform.position, path[actual].position, 2.5f * Time.deltaTime);
        }

        
    }

    private void FixedUpdate()
    {
        objetivo = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (Vector2.Distance(objetivo.transform.position, transform.position) < 10 && !exploding && !isWalkingSound)
        {
            reproductor.clip = walking;
            reproductor.loop = true;
            reproductor.volume = .1f;
            reproductor.Play();
            isWalkingSound = true;
        }
        else if (Vector2.Distance(objetivo.transform.position, transform.position) > 12)
        {
            reproductor.Stop();
            isWalkingSound = false;
        }
        if (Vector2.Distance(objetivo.transform.position, transform.position) < 3 && !exploding)
        {
            exploding = true;
            StartCoroutine(Explota());
        }

        if (stats.GetVidaActual() <= 0 && !dead)
        {
            StopAllCoroutines();
            dead = true;
            reproductor.clip = dying;
            reproductor.Play();
        }

    }
    IEnumerator Explota()
    {

        anim.SetTrigger("Explode");
        reproductor.Stop();
        reproductor.clip = tssSound;
        reproductor.loop = true;
        reproductor.volume = .6f;
        reproductor.Play();
        yield return new WaitForSeconds(1.1f); //tiempo antes de que explote
        reproductor.clip = explodeSound;
        reproductor.loop = false;
        reproductor.volume = 1;
        reproductor.Play();
        yield return new WaitForSeconds(.3f);
        Collider2D[] golpeados = Physics2D.OverlapCircleAll(transform.position, 3, jugador);
        for (int i = 0; i < golpeados.Length; i++)
        {
            if (formaActual.GetCurrentForm() == 1)
            {
                try
                {
                    golpeados[i].GetComponent<PlayerController>().ReceiveDamage(10);
                }
                catch
                {

                }
            }
            else if (formaActual.GetCurrentForm() == 2)
            {
                try //try catch porque desmadre de layers, le intenta hacer daño al trigger del jugador
                {
                    golpeados[i].GetComponent<MovimientoParrot>().ReceiveDamage(14);
                }
                catch
                {

                }
            }
            
        }
        yield return new WaitForSeconds(.3f);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 2.8f);
    }

    IEnumerator ChecarSiLlegue()
    {
        float d;
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            d = Vector2.Distance(
                transform.position,
                path[actual].position
                );
            if (d < rango)
            {
                Flip();
                actual++;
                actual %= path.Length;
            }

        }
    }

    void Flip()
    {
        if (lookingRight == true)
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            lookingRight = false;
            lookingLeft = true;
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
            lookingRight = true;
            lookingLeft = false;
        }
        
    }
}
