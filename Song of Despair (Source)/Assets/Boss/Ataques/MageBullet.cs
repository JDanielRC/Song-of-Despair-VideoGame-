using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform objetivo;
    private Vector2 direccion;
    private Rigidbody2D rb2d;
    //private float fuerza = 10f;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        objetivo = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        direccion = (objetivo.position - transform.position).normalized * 7;
        rb2d.velocity = new Vector2(direccion.x, direccion.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            StartCoroutine(Desaparece());
        }
    }

    IEnumerator Desaparece()
    {
        yield return new WaitForSeconds(.3f);
        Destroy(gameObject);
    }
}
