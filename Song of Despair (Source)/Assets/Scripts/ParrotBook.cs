using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrotBook : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().SetPerico(true);
            GameObject.FindGameObjectWithTag("Reproductor3").GetComponent<AudioTres>().PlayParrotBook();
            Destroy(gameObject);
        }
    }
}
