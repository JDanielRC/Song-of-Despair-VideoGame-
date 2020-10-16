using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaseUno : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform[] path; //punto en cada plataforma para dirigirse a ella
    private int actual;
    private Transform bulletPlace;
    private GameObject mageBullet;
    private AudioSource reproductor;
    private AudioClip bulletSound;
    void Start()
    {
        StartCoroutine(ChecarSiLlegue());
        StartCoroutine(FireBullets());
        actual = 0;
        path = GetComponent<Jefe>().path;
        bulletPlace = GetComponent<Jefe>().bulletPlace;
        mageBullet = GetComponent<Jefe>().mageBullet;
        reproductor = GetComponent<AudioSource>();
        bulletSound = GetComponent<Jefe>().bulletSound;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, path[actual].position, 3.1f * Time.deltaTime);
    }

    IEnumerator ChecarSiLlegue()
    {
        float d;
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            actual = GetComponent<Jefe>().GetActual();
            d = Vector2.Distance(transform.position, path[actual].position);
            if (d < 0.5f)
            {
                if (actual == 6)
                {
                    yield return new WaitForSeconds(4.5f); //espera ocho segundos en el centro del mapa
                    GetComponent<Jefe>().IncreaseActual(this.path.Length);
                }
                else
                {
                    yield return new WaitForSeconds(3.1f); //espera cinco segundos en la plataforma
                    GetComponent<Jefe>().IncreaseActual(this.path.Length);
                }
                
                
            }

        }
    }

    IEnumerator FireBullets()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            reproductor.clip = bulletSound;
            reproductor.volume = 1f;
            reproductor.Play();
            Instantiate(mageBullet, bulletPlace.position, bulletPlace.rotation);
        }
    }
}
