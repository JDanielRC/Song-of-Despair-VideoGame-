using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefe : MonoBehaviour
{
    // ELEMENTOS PRIVADOS DEL BOSS
    private ANodo actual;
    private Simbolo tresCuartosHP, cincuentaHP, veinticincoHP;
    private MonoBehaviour comportamiento;
    private Enemigos stats;
    private float porcentajeVida;
    private bool lookingRight;
    private Transform objetivo;
    private Animator anim;
    private int pathActual;

    //ESTADOS DEL JEFE
    private ANodo faseUno, faseDos, faseTres, faseFinal;

    //ELEMENTOS PUBLICOS
    public Transform[] path;
    public Transform[] acidPlaces;
    public Transform[] minionSpawn;
    public Transform bulletPlace;
    public GameObject mageBullet;
    public GameObject mageFire;
    public GameObject acido;
    public GameObject minion;
    public AudioClip bulletSound;
    void Start()
    {
        /*1era fase:
         * Se mueve entre puntos lentamente, permaneciendo en cada punto mucho tiempo, arroja balas al jugador
         
         2da fase:
         *Se mueve entre puntos de manera normal, permanece un poco menos en cada uno y empieza a spawnear cada cierto tiempo
         * fuego cerca del jugador, que se instancia en la pos del jugador pero tarda en aparecer un poco
         
         3era fase:
         * Se mueve un poco más rápido entre puntos, menos tiempo en cada plataforma, y ataque de acido
         
         Ultima fase:
         * Todo lo anterior y spawnea enemigos en las dos plataformas superiores cuando llega a ellas
         * 
        */
        //ESTADOS
        faseUno = new ANodo("FaseUno", typeof(FaseUno));
        faseDos = new ANodo("FaseDos", typeof(FaseDos));
        faseTres = new ANodo("FaseTres", typeof(FaseTres));
        faseFinal = new ANodo("FaseFinal", typeof(FaseFinal));

        //LENGUAJE
        tresCuartosHP = new Simbolo("75% de vida restante");
        cincuentaHP = new Simbolo("50% de vida restante");
        veinticincoHP = new Simbolo("25% de vida restante");

        //FUNCION DE TRANSICION   -  DETONADORES
        faseUno.AddTransicion(tresCuartosHP, faseDos);
        faseDos.AddTransicion(cincuentaHP, faseTres);
        faseTres.AddTransicion(veinticincoHP, faseFinal);



        //obtener datos

        anim = GetComponent<Animator>();
        stats = GetComponent<Enemigos>();
        StartCoroutine(Spawn());
        pathActual = 0;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        porcentajeVida = stats.GetVidaActual() / stats.GetVidaMaxima();
        if (porcentajeVida < .80f && porcentajeVida > .50f && actual != faseDos) //pasar de fase uno a fase dos
        {
            GameObject.FindGameObjectWithTag("Reproductor3").GetComponent<AudioTres>().PlaySecondPhaseYouu();
            CambiarEstado(tresCuartosHP);
        }
        if (porcentajeVida <= .50f && porcentajeVida > .25f && actual != faseTres) //pasar de fase dos a fase tres
        {
            GameObject.FindGameObjectWithTag("Reproductor3").GetComponent<AudioTres>().PlayThirdPhaseEnough();
            GameObject.FindGameObjectWithTag("MusicaFondo").GetComponent<MusicaFondo>().PlayPreEndingBoss();
            anim.SetTrigger("SecondPhase");
            CambiarEstado(cincuentaHP);
        }
        if (porcentajeVida <= .35f && actual != faseFinal)
        {
            
            GameObject.FindGameObjectWithTag("Reproductor3").GetComponent<AudioTres>().PlayFinalPhaseRoar();
            GameObject.FindGameObjectWithTag("MusicaFondo").GetComponent<MusicaFondo>().PlayEndingBoss();
            anim.SetTrigger("FinalPhase");
            CambiarEstado(veinticincoHP);

        }


        //DETECTA A JUGADOR Y ROTA PARA SIEMPRE VERLO
        GameObject objetivoX = GameObject.FindGameObjectWithTag("Player");
        if (objetivoX != null)
        {
            objetivo = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            Vector2 direccion = (objetivo.position - transform.position).normalized * 120f;
            if (direccion.x >= 0)
            {
                lookingRight = true;
                Flip();
            }
            else
            {
                lookingRight = false;
                Flip();
            }

        }


    }
    //FUNCION PARA VOLTEARSE 
    void Flip()
    {
        if (lookingRight == true)
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            lookingRight = false;
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
            lookingRight = true;
        }

    }

    void CambiarEstado(Simbolo simbolo)
    {
        ANodo temp = actual.AplicarTransicion(simbolo);
        if (actual != temp)
        {
            actual = temp;
            // deshacernos del behaviour actual
            Destroy(comportamiento);
            comportamiento = gameObject.AddComponent(actual.Comportamiento) as MonoBehaviour;

        }
        
    }

    IEnumerator Spawn()
    {
        //anim.SetTrigger("Spawn");
        yield return new WaitForSeconds(1.1f);
        GameObject.FindGameObjectWithTag("Reproductor3").GetComponent<AudioTres>().PlaySpawnPhaseLaugh();
        yield return new WaitForSeconds(4.9f);
        actual = faseUno;
        comportamiento = gameObject.AddComponent(actual.Comportamiento) as MonoBehaviour;
    }

    public int GetActual()
    {
        return this.pathActual;
    }

    public void IncreaseActual(int mod)
    {
        this.pathActual++;
        this.pathActual %= mod;
    }
}
