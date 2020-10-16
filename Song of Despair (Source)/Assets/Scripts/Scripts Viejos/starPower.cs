using System.Collections;
using UnityEngine;

public class starPower : MonoBehaviour
{

    private float mult = 1.4f;
    private float duration = 4f;

    private SpriteRenderer dibujin;

    public PlayerController jugador;

    private void Start()
    {
        jugador.jumPower = 250;
        dibujin = gameObject.GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(PickupJump(collision));
            
        }
    }

    IEnumerator PickupJump(Collider2D player)
    {

        
        jugador.jumPower *= mult;

        GetComponent<Collider2D>().enabled = false;

        dibujin.enabled = false;

        yield return new WaitForSeconds(duration);

        jugador.jumPower /= mult;

        Destroy(gameObject);

    }
}