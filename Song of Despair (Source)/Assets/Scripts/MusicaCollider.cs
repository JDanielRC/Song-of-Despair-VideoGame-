using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaCollider : MonoBehaviour
{
    // Start is called before the first frame update
    MusicaFondo reproductorFondo;
    void Start()
    {
        reproductorFondo = GameObject.FindGameObjectWithTag("MusicaFondo").GetComponent<MusicaFondo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TPCiudad")
        {
            reproductorFondo.PlayCiudad();
        } else if (collision.tag == "TPCasa")
        {
            reproductorFondo.PlayCasa();
        } else if (collision.tag == "TPBosque")
        {
            reproductorFondo.PlayBosque();
        } else if (collision.tag == "TPCementerio")
        {
            reproductorFondo.PlayCementerio();
        } else if (collision.tag == "TPCastillo")
        {
            reproductorFondo.PlayCastillo();
        } else if (collision.tag == "EntradaCueva")
        {
            reproductorFondo.PlayCavernas();
        } else if (collision.tag == "EntradaCueva2")
        {
            reproductorFondo.PlayCavernas2();
        } else if (collision.tag == "PreBoss")
        {
            reproductorFondo.PlayPreBoss();
        } else if (collision.tag == "FirstBoss")
        {
            reproductorFondo.PlayFirstBoss();
        }
    }
}
