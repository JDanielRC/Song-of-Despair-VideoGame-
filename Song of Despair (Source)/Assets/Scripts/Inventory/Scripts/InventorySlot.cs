using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    

    public Image icono;
    Item objetoGuardado;

    public void AddItem(Item objeto)
    {
        objetoGuardado = objeto;
        icono.sprite = objeto.icono;
        icono.enabled = true;
    }

    public void ClearSlot()
    {
        objetoGuardado = null;
        icono.sprite = null;
        icono.enabled = false;
    }

    public void UseItem()
    {
        if (objetoGuardado != null)
        {
            objetoGuardado.Usar();
        }
    }
}
