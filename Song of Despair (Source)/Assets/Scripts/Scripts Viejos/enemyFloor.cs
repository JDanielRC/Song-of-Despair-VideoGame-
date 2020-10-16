using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFloor : MonoBehaviour
{

    public float velocidad;
    private float distancia;

    private bool movingRight = true;

    public Transform limite;

    public respawn jugador;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(limite.position, Vector2.down, 2f);
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;

            }
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
