using System.Collections;
using UnityEngine;

public class gemPower : MonoBehaviour
{

    private float mult = 2f;
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
            StartCoroutine(PickupBig(collision));

        }
    }

    IEnumerator PickupBig(Collider2D player)
    {

        player.transform.localScale *= mult;

        GetComponent<Collider2D>().enabled = false;

        dibujin.enabled = false;

        yield return new WaitForSeconds(duration);

        player.transform.localScale /= mult;

        Destroy(gameObject);

    }
}