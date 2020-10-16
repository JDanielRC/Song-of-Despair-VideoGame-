using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutDoors : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] puertaDesactivada;
    private bool activated;
    private bool opening;
    private bool enemigosDead;
    public GameObject[] enemigos;
    public bool areaLibro;
    private int cont;
    void Start()
    {
        activated = false;
        cont = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        for (int i = 0; i < enemigos.Length; i++)
        {
            if (enemigos[i].gameObject != null)
            {
                cont = 0;
                break;
            }
            cont++;
            if (cont == enemigos.Length)
            {
                enemigosDead = true;
            }
        }
        if (enemigosDead && activated && !opening)
        {
            opening = true;
            StartCoroutine(AbrirPuertas());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !activated)
        {
            GameObject.FindGameObjectWithTag("Reproductor3").GetComponent<AudioTres>().PlayShutDoor();
            for (int i = 0; i < puertaDesactivada.Length; i++)
            {
                puertaDesactivada[i].SetActive(true);
            }
            activated = true;
            if (areaLibro)
            {
                GameObject.FindGameObjectWithTag("MusicaFondo").GetComponent<MusicaFondo>().PlayBattle();
            }
        }
    }

    IEnumerator AbrirPuertas()
    {
        yield return new WaitForSeconds(.3f);
        GameObject.FindGameObjectWithTag("Reproductor3").GetComponent<AudioTres>().PlayOpenDoor();
        for (int i = 0; i < puertaDesactivada.Length; i++)
        {
            puertaDesactivada[i].SetActive(false);
        }
        yield return new WaitForSeconds(1);
        if (areaLibro)
        {
            GameObject.FindGameObjectWithTag("MusicaFondo").GetComponent<MusicaFondo>().PlayCavernas();
        }
    }
}
