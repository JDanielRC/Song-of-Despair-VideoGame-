using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Key", menuName = "Inventory/Key")]
public class Key : Item
{
    // Start is called before the first frame update
    private Inventory inventario;
    private GameObject cerradura;
    private CambioForma manager;
    private ReproductorExterno sonidoManager;
    private Transform jugador;

    public override void Usar()
    {  //SOLUCION TEMPORAL
        inventario = Inventory.instance;
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        manager = GameObject.FindGameObjectWithTag("ShapeController").GetComponent<CambioForma>();
        sonidoManager = GameObject.FindGameObjectWithTag("ShapeController").GetComponent<ReproductorExterno>();

        if (manager.GetCurrentForm() == 1)
        {
            if (Vector2.Distance(jugador.transform.position, cerradura.transform.position) < 10)
            {
                base.Usar();
                sonidoManager.PlayOpenSound();
                cerradura.SetActive(false);
                inventario.Remove(this);
            }
        }
    }


    public void GiveMeLock(GameObject cerradura)
    {
        this.cerradura = cerradura;
    }
}

