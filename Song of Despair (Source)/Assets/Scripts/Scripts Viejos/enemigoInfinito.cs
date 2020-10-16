using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigoInfinito : MonoBehaviour
{

    public float velocidad;

    public respawn jugador;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * velocidad * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            jugador.Reaparecer();
        }

    }
}
