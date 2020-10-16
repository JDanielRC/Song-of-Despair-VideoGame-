using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigos : MonoBehaviour
{
    // Start is called before the first frame update
    public int vidaMaxima;
    private int vidaActual;
    public float velocidad;
    public float damage;
    public int expPoints;
    private bool isDead;
    private AudioSource reproductor;
    public AudioClip hurtSound;
    

    private Animator anim;
    void Start()
    {

        anim = GetComponent<Animator>();
        reproductor = GetComponent<AudioSource>();
        isDead = false;
        vidaActual = vidaMaxima;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (vidaActual <= 0 && !isDead)
        {
            isDead = true;
            StartCoroutine(MeMuero());
        }
    }

    public void TakeDamage(int poderAtaque)
    {
        reproductor.volume = .7f;
        reproductor.loop = false;
        reproductor.clip = hurtSound;
        reproductor.Play();
        anim.SetTrigger("Damaged");
        vidaActual -= poderAtaque;
    }

    IEnumerator MeMuero()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ReceiveExp(this.expPoints);
        anim.SetTrigger("Dying");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return this.damage;
    }

    public float GetVidaActual()
    {
        return this.vidaActual;
    }

    public float GetVidaMaxima()
    {
        return this.vidaMaxima;
    }

    public void HealEnemies()
    {
        this.vidaActual = vidaMaxima;
    }
}
