using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaseTres : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform[] path; //punto en cada plataforma para dirigirse a ella
    private Transform[] acidPlaces;
    private int actual;
    private Transform bulletPlace;
    private GameObject mageBullet;
    private GameObject mageFire;
    private GameObject acido;
    private AudioSource reproductor;
    private AudioClip bulletSound;
    void Start()
    {
        StartCoroutine(ChecarSiLlegue());
        StartCoroutine(FireBullets());
        StartCoroutine(CreateFire());
        StartCoroutine(AcidPlaces());
        actual = GetComponent<Jefe>().GetActual();
        path = GetComponent<Jefe>().path;
        bulletPlace = GetComponent<Jefe>().bulletPlace;
        mageBullet = GetComponent<Jefe>().mageBullet;
        mageFire = GetComponent<Jefe>().mageFire;
        acidPlaces = GetComponent<Jefe>().acidPlaces;
        acido = GetComponent<Jefe>().acido;
        reproductor = GetComponent<AudioSource>();
        bulletSound = GetComponent<Jefe>().bulletSound;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, path[actual].position, 4.4f * Time.deltaTime); //se mueve 1.5 unidas mas rapido que antes
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
                    yield return new WaitForSeconds(3.3f); //espera 5.4 segundos en el centro del mapa
                    GetComponent<Jefe>().IncreaseActual(this.path.Length);
                }
                else
                {
                    yield return new WaitForSeconds(1.8f); //espera tres segundos en la plataforma
                    GetComponent<Jefe>().IncreaseActual(this.path.Length);
                }
            }

        }
    }

    IEnumerator FireBullets()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.1f); //arroja balas magicas cada 2.4 segundos
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
            yield return new WaitForSeconds(3.1f); //hace aparecer el fuego cada 3.5 segundos (dura activo 2 segundos y el tiempo de advertencia es de 1.2)
            Transform position = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            Instantiate(mageFire, position.position, position.rotation);
        }
    }

    IEnumerator AcidPlaces()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.8f); //acido cada 4.6 segundos
            int index = Random.Range(0, acidPlaces.Length);
            Instantiate(acido, acidPlaces[index].transform.position, acido.transform.rotation);
            
        }
    }
}
