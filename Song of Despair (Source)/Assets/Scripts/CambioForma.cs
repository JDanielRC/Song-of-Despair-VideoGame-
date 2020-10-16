using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioForma : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerController jugador;
    private MovimientoParrot perico;
    private float CD;
    public GameObject knight;
    public GameObject parrot;
    public GameObject dog;
    private int shiftNum;
    void Start()
    {
        jugador = knight.GetComponent<PlayerController>();
        perico = parrot.GetComponent<MovimientoParrot>();
        knight.SetActive(true);
        parrot.SetActive(false);
        shiftNum = 1;
    }

    // Update is called once per frame
    void Update()
    {
        CD -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {

            if (shiftNum == 1 && CD < 0 && jugador.GetPerico())
            {
                perico = parrot.GetComponent<MovimientoParrot>();
                knight.SetActive(false);
                parrot.SetActive(true);
                parrot.transform.position = knight.transform.position;
                parrot.transform.rotation = knight.transform.rotation;
                perico.SetHealth(jugador.GetVidaActual());
                shiftNum = 2;
                CD = 3;
            }
            else if (shiftNum == 2 && CD < 0)
            {
                jugador = knight.GetComponent<PlayerController>();
                parrot.SetActive(false);
                knight.SetActive(true);
                Vector3 posicion = parrot.transform.position;
                posicion.y += 1;
                knight.transform.position = posicion;
                knight.transform.rotation = parrot.transform.rotation;
                jugador.SetHealth(perico.GetVidaActual());

                shiftNum = 1;
                CD = 3;
            }
        }
    }

    public int GetCurrentForm()
    {
        return this.shiftNum;
    }
}
