using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaseDos : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform[] path; //punto en cada plataforma para dirigirse a ella
    private int actual;
    private Transform bulletPlace;
    private GameObject mageBullet;
    private GameObject mageFire;
    private AudioSource reproductor;
    private AudioClip bulletSound;
    void Start()
    {
        StartCoroutine(ChecarSiLlegue());
        StartCoroutine(FireBullets());
        StartCoroutine(CreateFire());
        actual = GetComponent<Jefe>().GetActual();
        path = GetComponent<Jefe>().path;
        bulletPlace = GetComponent<Jefe>().bulletPlace;
        mageBullet = GetComponent<Jefe>().mageBullet;
        reproductor = GetComponent<AudioSource>();
        mageFire = GetComponent<Jefe>().mageFire;
        bulletSound = GetComponent<Jefe>().bulletSound;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, path[actual].position, 3.6f * Time.deltaTime); //se mueve 1 unidas mas rapido que antes
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
                    yield return new WaitForSeconds(3.5f); //espera 6.5 segundos en el centro del mapa
                    GetComponent<Jefe>().IncreaseActual(this.path.Length);
                }
                else
                {
                    yield return new WaitForSeconds(2.7f); //espera cuatro segundos en la plataforma
                    GetComponent<Jefe>().IncreaseActual(this.path.Length);
                }
            }

        }
    }

    IEnumerator FireBullets()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.6f); //arroja balas magicas cada 2.8 segundos
            reproductor.clip = bulletSound;
            reproductor.volume = 1f;
            reproductor.Play();
            Instantiate(mageBullet, bulletPlace.position, bulletPlace.rotation);
        }
    }

    IEnumerator CreateFire()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.5f); //hace aparecer el fuego cada 4.5 segundos (dura activo 2 segundos y el tiempo de advertencia es de 1.2)
            Transform position = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
            Instantiate(mageFire, position.position, position.rotation);
        }
    }
}