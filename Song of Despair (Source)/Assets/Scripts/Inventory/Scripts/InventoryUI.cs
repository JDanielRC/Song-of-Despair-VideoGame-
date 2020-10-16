using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform PanelInventario;
    public GameObject MenuInventario;

    Inventory inventario;
    InventorySlot[] slots;

    void Start()
    {
        inventario = Inventory.instance;
        inventario.onItemChangedCallback += UpdateUI;

        slots = PanelInventario.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (MenuInventario.activeSelf == false)
            {
                GameObject.FindGameObjectWithTag("Reproductor3").GetComponent<AudioTres>().PlayOpenInventory();
            }
            MenuInventario.SetActive(!MenuInventario.activeSelf);
        }
    }

    void UpdateUI()
    {
        slots = PanelInventario.GetComponentsInChildren<InventorySlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventario.objetos.Count)
            {
                slots[i].AddItem(inventario.objetos[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
        //print("Actualizando Inventario");
    }
}
