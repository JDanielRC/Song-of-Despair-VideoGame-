using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{

    public Sprite red, green;
    private SpriteRenderer bandera;

    // Start is called before the first frame update
    void Start()
    {
        bandera = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            bandera.sprite = green;
        }

    }
}
