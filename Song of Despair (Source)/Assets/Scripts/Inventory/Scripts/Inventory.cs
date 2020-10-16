using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update

    #region InstanciaInventario
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Hay mas de un inventario");
            return;
        }
        instance = this;
    }

    #endregion

    public int slots = 20;
    public List<Item> objetos = new List<Item>();

    public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;
    

    public bool Add(Item item)
    {
        if (objetos.Count >= slots)
        {
            print("No hay suficiente espacio");
            return false;
        }
        objetos.Add(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
        return true;
    }
    
    public void Remove(Item item)
    {
        objetos.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
