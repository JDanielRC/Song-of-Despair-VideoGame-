using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    #region InstanciaPrincipal

    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    Equipment[] equipamentoActual;
    Inventory inventario;
    AtaqueJugador statsJugador;

    private void Start()
    {
        int numeroSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length; //agarra cuántos valores hay en la enumeracion de equipment
        equipamentoActual = new Equipment[numeroSlots];
        inventario = Inventory.instance;
        statsJugador = GameObject.FindGameObjectWithTag("Player").GetComponent<AtaqueJugador>();
    }

    public void Equipar(Equipment objeto)
    {
        int index = (int) objeto.equipSlot;

        Equipment objetoViejo = null;
        if (equipamentoActual[index] != null)
        {
            objetoViejo = equipamentoActual[index];
            inventario.Add(objetoViejo);

        }
        

        equipamentoActual[index] = objeto;
        if (index == 3)
        {
            statsJugador.SetPoderAtaque(objeto.damageModifier);
            statsJugador.SetRangoEspada(objeto.rangoModifier);
        }

        if (index == 1)
        {
            statsJugador.SetArmadura(objeto.armorModifier);
        }
    }

}
