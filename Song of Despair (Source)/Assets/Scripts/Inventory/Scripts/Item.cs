using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string nombre = "Nuevo objeto";
    public Sprite icono;

    public virtual void Usar()
    {
        //hace algo
        //virtual nos ayuda a que hagan cosas diferentes

        //Debug.Log("Usaste el " + nombre);
    }

    public void QuitarInventario()
    {
        Inventory.instance.Remove(this);
        //Debug.Log("quite " + this.nombre);
    }

}
