using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    #region Stats
    public float maxSpeed = 30;
    public float speed = 500f;
    public float jumPower = 350;
    private float cooldownAttack = .6f;
    private float cooldownDamaged = 1.2f;
    private float vidaActual = 350;
    private float vidaMaxima = 350;
    private int currentExp;
    private int maxExp = 40;
    private int level;
    private int armadura;

    #endregion

    #region Propiedades
    public bool pisado;
    private bool dead = false;
    private bool running = false;
    private bool reproduciendo = false;
    private bool crouching;
    private bool executing;
    private bool perico;
    #endregion

    private AudioSource reproductor;
    private ReproductorExterno sonidoManager;
    private AudioDos sonidoManagerDos;
    public AudioClip audioCorriendo;
    public AudioClip audioAtacando;

    public Text exp;
    public Text vidaLvl;

    public Vector2 spawn;
    public Vector2 checkPoint;

    private Animator anim;
    private Rigidbody2D rb2D;
    private RigidbodyConstraints2D originales;

    public GameObject panelPerdio, panelGano;

    // Start is called before the first frame update
    void Start()
    {
        sonidoManager = GameObject.FindGameObjectWithTag("ShapeController").GetComponent<ReproductorExterno>();
        sonidoManagerDos = GameObject.FindGameObjectWithTag("Reproductor2").GetComponent<AudioDos>();
        reproductor = GetComponent<AudioSource>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        spawn = transform.position;
        checkPoint = spawn;
        anim = GetComponent<Animator>();
        crouching = false;
        Time.timeScale = 1;
        jumPower = 380;
        maxExp = 40;
        currentExp = 0;
        level = 1;
        armadura = 0;
        executing = false;
        perico = false;
        this.vidaLvl.text = "Lvl: " + this.level + "   HP: " + this.vidaActual + "/" + this.vidaMaxima;
        this.exp.text = "Current XP: " + this.currentExp + " Next level: " + (this.maxExp - this.currentExp);
    }

    // Update is called once per frame
    void Update()
    {
        cooldownAttack -= Time.deltaTime;
        cooldownDamaged -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && pisado && !crouching)
        {
            rb2D.AddForce(Vector2.up * jumPower);
            anim.SetTrigger("jump");
            reproductor.Stop();
            reproduciendo = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && pisado && cooldownDamaged < -0.5f)
        {
            originales = rb2D.constraints;
            anim.SetTrigger("Crouch");
            rb2D.constraints = RigidbodyConstraints2D.FreezePositionX;
            crouching = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && crouching)
        {
            anim.SetTrigger("StopCrouch");
            rb2D.constraints = originales;
            crouching = false;
        }
        
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.E) && cooldownAttack <= 0)
        {
            sonidoManager.PlayAttackSound();  //sonido ataque
        }

        if (running && !reproduciendo && !crouching && pisado)
        {
            StartCoroutine(ReproducirCorriendo());
        }


        if (h != 0 )
        {
            running = true;
        }
        else if (running && h == 0)
        {
            reproductor.Stop();
            running = false;
            reproduciendo = false;
            reproductor.loop = false;
        }
        anim.SetFloat("Walking", h);

        /*
        rb2D.AddForce((Vector2.right * speed) * h);

        if (rb2D.velocity.x > maxSpeed)
        {
            rb2D.velocity = new Vector2(maxSpeed, rb2D.velocity.y);
        }

        if (rb2D.velocity.x < -maxSpeed)
        {
            rb2D.velocity = new Vector2(-maxSpeed, rb2D.velocity.y);
        }

        */
        if (!dead && !crouching)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + (Time.deltaTime * h * 5), transform.position.y), 2f);
        }
        if (h < 0 && !dead)
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
        else if (h > 0.1 && !dead)
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }

        if (vidaActual <= 0 && !dead && !executing)
        {
            executing = true;
            reproductor.Stop();
            dead = true;
            GameObject.FindGameObjectWithTag("MusicaFondo").GetComponent<MusicaFondo>().PlayMuerte();
            rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            StartCoroutine(JugadorMuerte());
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")
        {
            checkPoint = collision.transform.position;
        }

        if (collision.tag == "winTemporal")
        {
            reproductor.Stop();
            
            rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            panelGano.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        if (collision.tag == "ExperiencePoint")
        {
            Destroy(collision.gameObject);
            ReceiveExp(2);
            sonidoManagerDos.PlayExpSound();
            Destroy(collision.gameObject);
        }

        if (collision.tag == "Checkpoint")
        {
            this.checkPoint = collision.transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemigo" && cooldownDamaged <= 0 && !dead)
        {
            sonidoManager.PlayHurtSound();
            StartCoroutine(Damaged(collision.GetComponent<Enemigos>().GetDamage()));
        }

        if (collision.tag == "AtaqueEnemigo" && cooldownDamaged <= 0 && !dead)
        {
            sonidoManager.PlayHurtSound();
            StartCoroutine(Damaged(collision.GetComponent<AtaqueEnemigo>().GetDamage()));
        }

        if (collision.tag == "Pinchos" && cooldownDamaged <= 0 && !dead)
        {
            
            sonidoManager.PlayHurtSound();
            StartCoroutine(Damaged(collision.GetComponent<AtaqueEnemigo>().GetDamage()));
            print("herido por pinchos");
            print("daño: " + collision.GetComponent<AtaqueEnemigo>().GetDamage());
        }
    }

    

    IEnumerator JugadorMuerte()
    {
        sonidoManagerDos.PlayPlayerDeath();
        anim.SetTrigger("muerte");
        yield return new WaitForSeconds(1.8f);
        
        panelPerdio.gameObject.SetActive(true);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Time.timeScale = 0;
        //Destroy(gameObject);
    }

    IEnumerator Damaged(float enemyDmg)
    {
        enemyDmg -= armadura;
        if (enemyDmg < 0)
        {
            enemyDmg = 0;
        }
        vidaActual -= enemyDmg;
        this.vidaLvl.text = "Lvl: " + this.level +  " HP: " + this.vidaActual + "/" + this.vidaMaxima;
        anim.SetTrigger("damaged");
        cooldownDamaged = 1.2f;
        yield return null;
    }

    IEnumerator ReproducirCorriendo()
    {
        reproductor.clip = audioCorriendo;
        reproductor.loop = true;
        reproductor.volume = .15f;
        reproductor.Play();
        reproduciendo = true;
        yield return new WaitForSeconds(0);
    }
    /*IEnumerator ReproducirAtaque()
    {
        reproductor.clip = audioAtacando;
        reproductor.volume = .6f;
        reproductor.loop = false;
        reproductor.Play();
        yield return new WaitForSeconds(reproductor.clip.length);
    }
    */

    /*IEnumerator ReproducirAtaqueCorriendo()
    {
        StartCoroutine(ReproducirAtaque());
        yield return new WaitForSeconds(reproductor.clip.length);
        StartCoroutine(ReproducirCorriendo());
    }
    */
    public void Reaparecer()
    {
        transform.position = checkPoint;
        this.vidaActual = vidaMaxima;
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
        for (int i = 0; i < enemigos.Length; i++)
        {
            enemigos[i].GetComponent<Enemigos>().HealEnemies();
        }
        rb2D.constraints = originales;
        rb2D.freezeRotation = true;
        this.vidaLvl.text = "Lvl: " + this.level + " HP: " + this.vidaActual + "/" + this.vidaMaxima;
        dead = false;
        executing = false;
        panelPerdio.SetActive(false);
    }
    public void ReceiveDamage(int damage)
    {
        damage -= armadura;
        if (damage < 0)
        {
            damage = 0;
        }
        anim.SetTrigger("damaged");
        sonidoManager.PlayHurtSound();
        this.vidaActual -= damage;
        this.vidaLvl.text = "Lvl: " + this.level + " HP: " + this.vidaActual + "/" + this.vidaMaxima;
    }
    public void PickUp(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        Inventory.instance.Add(item);
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
        this.vidaLvl.text = "Lvl: " + this.level + "   HP: " + this.vidaActual + "/" + this.vidaMaxima;
    }

    public float GetCooldown()
    {
        return this.cooldownAttack;
    }

    public void SetCooldownAttack()
    {
        this.cooldownAttack = .75f;
    }

    public Animator GetAnimator()
    {
        return this.anim;
    }

    public float GetVidaActual()
    {
        return this.vidaActual;
    }

    public void SetArmadura(int armor)
    {
        this.armadura = armor;
    }

    public int GetArmadura()
    {
        return this.armadura;
    }

    public float GetVidaMaxima()
    {
        return this.vidaMaxima;
    }

    public bool GetCrouching()
    {
        return this.crouching;
    }

    public bool IsDead()
    {
        return this.dead;
    }

    public void SetHealth(float vida)
    {
        this.vidaActual = vida;
        this.vidaLvl.text = "Lvl: " + this.level + "   HP: " + this.vidaActual + "/" + this.vidaMaxima;
    }

    private void LevelUp()
    {
        GameObject.FindGameObjectWithTag("Reproductor3").GetComponent<AudioTres>().PlayLevelUp();
        this.currentExp -= maxExp;
        this.maxExp = (this.maxExp / 3) + this.maxExp;
        this.vidaMaxima += 15;
        Heal(((int)(this.vidaMaxima * .25f)));
        level++;
        if (level % 2 == 0)
        {
            GetComponent<AtaqueJugador>().IncreaseBaseAttack();
        }
        this.vidaLvl.text = "Lvl: " + this.level + " HP: " + this.vidaActual + "/" + this.vidaMaxima;
        if (this.currentExp >= maxExp)
        {
            LevelUp();
        }
    }

    public void ReceiveExp(int exp)
    {
        this.currentExp += exp;
        if (this.currentExp >= this.maxExp){
            LevelUp();
        }
        this.exp.text = "Current XP: " + this.currentExp + " Next level: " + (this.maxExp - this.currentExp);
    }

    public bool GetPerico()
    {
        return this.perico;
    }

    public void SetPerico(bool estado)
    {
        this.perico = estado;
    }

    public int GetLevel()
    {
        return this.level;
    }

    public void SetLevel(int amount)
    {
        this.level = amount;
    }

    public void SetVidaMaxima(float amount)
    {
        this.vidaMaxima = amount;
    }

    public void SetCheckPoint(Vector2 place)
    {
        this.checkPoint = place;
    }

    public Vector2 GetCheckPoint()
    {
        return this.checkPoint;
    }
}
