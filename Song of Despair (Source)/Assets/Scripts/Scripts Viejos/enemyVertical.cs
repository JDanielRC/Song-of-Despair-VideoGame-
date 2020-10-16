using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyVertical : MonoBehaviour
{

    public float velocidad;
    private int direccion;
    private Vector2 posicion;

    public respawn jugador;
    // Start is called before the first frame update
    void Start()
    {
        posicion = transform.position;
        direccion = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * velocidad * Time.deltaTime * direccion);
        if (transform.position.y > 2)
        {
            direccion = -1;
        }
        else if (transform.position.y < -4.5)
        {
            direccion = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            jugador.Reaparecer();
        }

    }
}
