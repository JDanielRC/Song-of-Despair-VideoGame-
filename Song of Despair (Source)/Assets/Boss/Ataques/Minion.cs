using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    private Transform objetivo;
    private Enemigos stats;
    private bool dead;
    void Start()
    {
        stats = GetComponent<Enemigos>();
        dead = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stats.GetVidaActual() <= 0)
        {
            dead = true;
        }

        GameObject objetivoX = GameObject.FindGameObjectWithTag("Player");
        if (objetivoX != null && !dead)
        {
            objetivo = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            transform.position = Vector2.MoveTowards(transform.position, objetivo.position, .99f * Time.deltaTime);
            if ((transform.position - objetivo.position).x < 0)
            {
                transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            }
            else if ((transform.position - objetivo.position).x > 0)
            {
                transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
            }
        }

    }
}
