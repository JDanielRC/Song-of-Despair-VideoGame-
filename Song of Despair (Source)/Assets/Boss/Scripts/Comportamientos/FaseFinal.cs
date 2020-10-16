using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaseFinal : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform[] path; //punto en cada plataforma para dirigirse a ella
    private Transform[] acidPlaces;
    private Transform[] minionSpawn;
    private int actual;
    private Transform bulletPlace;
    private GameObject mageBullet;
    private GameObject mageFire;
    private GameObject acido;
    private GameObject minion;
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
        minionSpawn = GetComponent<Jefe>().minionSpawn;
        minion = GetComponent<Jefe>().minion;
        reproductor = GetComponent<AudioSource>();
        bulletSound = GetComponent<Jefe>().bulletSound;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, path[actual].position, 5.8f * Time.deltaTime); //se mueve 2.7 unidas mas rapido que el principio
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
                if (actual == 1)
                {
                    StartCoroutine(SpawnMinion(minionSpawn[0])); //spawnear minion en posicion izquierda
                } else if (actual == 4)
                {
                    StartCoroutine(SpawnMinion(minionSpawn[1])); //spawnear minion en posicion derecha
                } else if (actual == 6)
                {
                    StartCoroutine(SpawnMinion(minionSpawn[2]));
                }
                if (actual == 6)
                {
                    yield return new WaitForSeconds(2f); //espera 4 segundos en el centro del mapa
                    GetComponent<Jefe>().IncreaseActual(this.path.Length);
                }
                else
                {
                    yield return new WaitForSeconds(1f); //espera 2.4 segundos en la plataforma
                    GetComponent<Jefe>().IncreaseActual(this.path.Length);
                }
            }

        }
    }

    IEnumerator FireBullets()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f); //arroja balas magicas cada 5 segundos (no quiero abrumar al jugador)
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
            yield return new WaitForSeconds(2.5f); //hace aparecer el fuego cada 3 segundos (dura activo 2 segundos y el tiempo de advertencia es de 1.2)
            Transform position = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            Instantiate(mageFire, position.position, position.rotation);
        }
    }

    IEnumerator AcidPlaces()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.9f); //acido cada 3.8 segundos
            int index = Random.Range(0, acidPlaces.Length);
            Instantiate(acido, acidPlaces[index].transform.position, acido.transform.rotation);

        }
    }

    IEnumerator SpawnMinion(Transform position)
    {
        GameObject.FindGameObjectWithTag("UltimoReproductor").GetComponent<UltimoReproductor>().PlaySummoningSound();
        yield return new WaitForSeconds(2.1f);
        Instantiate(minion, position.position, position.rotation);
        yield return null;
    }
}
