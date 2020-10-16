using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovimientoParrot : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerController referenciaJugador;
    private float vidaActual;
    private float vidaMaxima;
    private CambioForma manager;
    private bool dead;
    private float cooldownDamaged = 1.2f;
    private Rigidbody2D rb2D;
    private AudioSource reproductor;
    private int level;
    private Animator anim;
    public AudioClip flap;
    public AudioClip damaged;
    public GameObject panel;
    public Text vidaText;
    public Vector2 checkPoint;
    private bool executing;

    public GameObject jugador;

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("ShapeController").GetComponent<CambioForma>();
        referenciaJugador = manager.knight.GetComponent<PlayerController>();
        vidaActual = referenciaJugador.GetVidaActual();
        vidaMaxima = referenciaJugador.GetVidaMaxima();
        level = referenciaJugador.GetLevel();
        anim = GetComponent<Animator>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        reproductor = GetComponent<AudioSource>();
        dead = false;
        executing = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!reproductor.isPlaying)
        {
            reproductor.clip = flap;
            reproductor.volume = .8f;
            reproductor.loop = true;
            reproductor.Play();
        }
        cooldownDamaged -= Time.deltaTime;

        if (vidaActual <= 0 && !dead && !executing)
        {
            print("me mori");
            executing = true;
            dead = true;
            //rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            GameObject.FindGameObjectWithTag("MusicaFondo").GetComponent<MusicaFondo>().PlayMuerte();
            StartCoroutine(ParrotDeath());
        }
        float h = Input.GetAxis("Horizontal");
        float x = Input.GetAxis("Vertical");
        
        if (!dead)
        {
            print("moviendome");

            transform.Translate(Mathf.Abs(h) * Time.deltaTime * 5, x * Time.deltaTime * 5, 0);
        }

        

        if (h < 0)
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
        else if (h > 0.1)
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }

        
    }

    IEnumerator ParrotDeath()
    {
        anim.SetTrigger("Death");
        yield return new WaitForSeconds(1.1f);
        Time.timeScale = 0;
        panel.gameObject.SetActive(true);

        //Destroy(gameObject);
    }

    public void Reaparecer()
    {
        transform.position = checkPoint;
        this.vidaActual = vidaMaxima;
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
        for (int i = 0; i < enemigos.Length; i++)
        {
            enemigos[i].GetComponent<Enemigos>().HealEnemies();
        }
        this.vidaText.text = "Lvl: " + referenciaJugador.GetLevel() + " HP: " + referenciaJugador.GetVidaActual() + "/" + referenciaJugador.GetVidaMaxima();
        dead = false;
        executing = false;
        panel.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemigo" && cooldownDamaged <= 0 && !dead)
        {

            print("recibi daño");
            StartCoroutine(Damaged(collision.GetComponent<Enemigos>().GetDamage()));
            this.vidaText.text = "Lvl: " + referenciaJugador.GetLevel() + " HP: " + referenciaJugador.GetVidaActual() + "/" + referenciaJugador.GetVidaMaxima();
        }

        if (collision.tag == "AtaqueEnemigo" && cooldownDamaged <= 0 && !dead)
        {
            print("me pego su ataque");
            StartCoroutine(Damaged(collision.GetComponent<AtaqueEnemigo>().GetDamage()));
            this.vidaText.text = "Lvl: " + referenciaJugador.GetLevel() + " HP: " + referenciaJugador.GetVidaActual() + "/" + referenciaJugador.GetVidaMaxima();
        }

        if (collision.transform.tag == "Puente")
        {
            StartCoroutine(CambiarLayerPuente(collision));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")
        {
            checkPoint = collision.transform.position;
        }
    }

    IEnumerator CambiarLayerPuente(Collider2D collision)
    {
        collision.gameObject.layer = 11;
        yield return new WaitForSeconds(.2f);
        collision.gameObject.layer = 0;
    }

    IEnumerator Damaged(float enemyDmg)
    {
        reproductor.loop = false;
        reproductor.clip = damaged;
        reproductor.Play();
        anim.SetTrigger("Damaged");
        vidaActual -= enemyDmg;
        cooldownDamaged = 1.2f;
        yield return null;
        this.vidaText.text = "Lvl: " + referenciaJugador.GetLevel() + " HP: " + referenciaJugador.GetVidaActual() + "/" + referenciaJugador.GetVidaMaxima();
    }

    public void ReceiveDamage(float damage)
    {
        reproductor.loop = false;
        reproductor.clip = damaged;
        reproductor.Play();
        anim.SetTrigger("Damaged");
        this.vidaActual -= damage;
        this.vidaText.text = "Lvl: " + referenciaJugador.GetLevel() + " HP: " + referenciaJugador.GetVidaActual() + "/" + referenciaJugador.GetVidaMaxima();
    }

    public float GetVidaActual()
    {
        return this.vidaActual;
    }

    public float GetVidaMaxima()
    {
        return this.vidaMaxima;
    }

    public void Heal(float cantidad)
    {
        if (this.vidaActual + cantidad > this.vidaMaxima)
        {
            this.vidaActual = vidaMaxima;
        }
        else
        {
            this.vidaActual += cantidad;
        }
        this.vidaText.text = "Lvl: " + referenciaJugador.GetLevel() + " HP: " + referenciaJugador.GetVidaActual() + "/" + referenciaJugador.GetVidaMaxima();
    }

    public bool IsDead()
    {
        return this.dead;
    }

    public void SetHealth(float vida)
    {
        this.vidaActual = vida;
        this.vidaText.text = "Lvl: " + referenciaJugador.GetLevel() + " HP: " + referenciaJugador.GetVidaActual() + "/" + referenciaJugador.GetVidaMaxima();
    }
}
