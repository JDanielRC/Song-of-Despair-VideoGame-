using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueJugador : MonoBehaviour
{
    // Start is called before the first frame update


    public Transform posicionEspada;
    public LayerMask enemigos;
    public LayerMask objetos;
    private PlayerController jugador;
    public float rangoAtaque;
    private int ataqueModificado;
    private int ataqueBase;
    private int poderEspada;

    void Start()
    {
        jugador = GetComponent<PlayerController>();
        ataqueBase = 22;
        rangoAtaque = .85f;
        ataqueModificado = ataqueBase;
        poderEspada = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (jugador.GetCooldown() <= 0)
        {
            if (Input.GetKeyDown(KeyCode.E) && !jugador.IsDead())
            {
                jugador.GetAnimator().SetTrigger("ataque");
                Collider2D[] enemigosGolpeados = Physics2D.OverlapCircleAll(posicionEspada.position, rangoAtaque, enemigos);
                for (int i = 0; i < enemigosGolpeados.Length; i++)
                {
                    enemigosGolpeados[i].GetComponent<Enemigos>().TakeDamage(ataqueModificado);
                }
                jugador.SetCooldownAttack();

                Collider2D[] objetosGolpeados = Physics2D.OverlapCircleAll(posicionEspada.position, rangoAtaque, objetos);
                for (int i = 0; i < objetosGolpeados.Length; i++)
                {
                    objetosGolpeados[i].GetComponent<Breakable>().WasHit();
                }
            }
        }
        
    }

    public void SetPoderAtaque(int modificador)
    {
        this.poderEspada = modificador;
        this.ataqueModificado = modificador + ataqueBase;
    }

    public void SetRangoEspada(float rango)
    {
        this.rangoAtaque = rango;
    }

    public void IncreaseBaseAttack()
    {
        this.ataqueBase++;
        SetPoderAtaque(this.poderEspada);
    }

    public void SetArmadura(int modificador)
    {
        this.jugador.SetArmadura(modificador);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(posicionEspada.position, rangoAtaque);
    }

    public int GetAtaqueBase()
    {
        return this.ataqueBase;
    }

    public void SetAtaqueBase(int amount)
    {
        this.ataqueBase = amount;

    }


}
