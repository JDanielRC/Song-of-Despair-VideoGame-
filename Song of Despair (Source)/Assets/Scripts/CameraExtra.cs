using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraExtra : MonoBehaviour
{
    // Start is called before the first frame update
    private CinemachineVirtualCamera camara;
    private GameObject jugador;
    private GameObject pantallaNegra;
    public bool teleporting;
    private bool executing;
    void Start()
    {
        camara = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!teleporting)
        {
            jugador = GameObject.FindGameObjectWithTag("Player");
            if (jugador != null)
            {
                camara.Follow = jugador.transform;
            }
        } else if (teleporting && !executing)
        {
            executing = true;
            pantallaNegra = GameObject.FindGameObjectWithTag("PuntoNegro");
            
            if (pantallaNegra != null)
            {
                camara.Follow = pantallaNegra.transform;
                StartCoroutine(LoadTime());
            }
        }

    }

    IEnumerator LoadTime()
    {
        yield return new WaitForSeconds(.5f);
        teleporting = false;
        executing = false;
    }
}
