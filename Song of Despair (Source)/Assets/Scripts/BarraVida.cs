using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerController jugador;
    private MovimientoParrot perico;
    private CambioForma manager;
    public Image content;
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("ShapeController").GetComponent<CambioForma>();
        jugador = manager.knight.GetComponent<PlayerController>();
        perico = manager.parrot.GetComponent<MovimientoParrot>();
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleBar();
    }

    private void HandleBar()
    {
        if (manager.GetCurrentForm() == 1)
        {
            content.fillAmount = MapeoVida(jugador.GetVidaActual(), 0, jugador.GetVidaMaxima(), 0, 1);
        } else if (manager.GetCurrentForm() == 2) {
            content.fillAmount = MapeoVida(perico.GetVidaActual(), 0, perico.GetVidaMaxima(), 0, 1);
        }
        
    }

    private float MapeoVida(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        // (80 de vida actual - 0 minimo de vida) * (1 maxima escala barra - 0 minima escala barra) / (100 maximo vida - 0 minimo de vida) + minimo escala barra
    }

    public void SetBarraVida()
    {
        this.content = GameObject.FindGameObjectWithTag("Vida").GetComponent<Image>();
    }
}
