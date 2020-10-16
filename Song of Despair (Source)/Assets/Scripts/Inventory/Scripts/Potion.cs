using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Potion")]
public class Potion : Item
{
    // Start is called before the first frame update
    public int healAmount;
    private PlayerController jugador;
    private MovimientoParrot jugadorPerico;
    private CambioForma manager;
    private ReproductorExterno sonidoManager;

    public override void Usar()
    {
        Debug.Log("usando pocion");
        base.Usar();
        manager = GameObject.FindGameObjectWithTag("ShapeController").GetComponent<CambioForma>();
        sonidoManager = GameObject.FindGameObjectWithTag("ShapeController").GetComponent<ReproductorExterno>();
        if (manager.GetCurrentForm() == 1)
        {
            jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            jugador.Heal((int) (healAmount + (jugador.GetVidaMaxima() * .25f)));
            sonidoManager.PlayHealSound();
            QuitarInventario();
        } else if (manager.GetCurrentForm() == 2)
        {
            jugadorPerico = GameObject.FindGameObjectWithTag("Player").GetComponent<MovimientoParrot>();
            jugadorPerico.Heal(healAmount);
            sonidoManager.PlayHealSound();
            QuitarInventario();
        }
    }
}
