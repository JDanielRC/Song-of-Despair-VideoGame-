using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlace : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform city;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Transform>().position = city.position;
            GameObject.FindGameObjectWithTag("CMCAM").GetComponent<CameraExtra>().teleporting = true;
        }
    }
}
