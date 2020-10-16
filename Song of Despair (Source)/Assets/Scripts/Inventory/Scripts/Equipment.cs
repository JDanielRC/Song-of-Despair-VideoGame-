using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{



    public EquipmentSlot equipSlot; // slot del equipamento

    public int armorModifier;      
    public int damageModifier;
    public float rangoModifier;

    // When pressed in inventory
    public override void Usar()
    {
        base.Usar();
        EquipmentManager.instance.Equipar(this);  // equiparlo
        GameObject.FindGameObjectWithTag("Reproductor3").GetComponent<AudioTres>().PlayEquipItem();
        QuitarInventario(); // lo quita del inventario después de equipar
    }

}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet }
