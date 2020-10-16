using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PisadoCheck : MonoBehaviour
{
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {

        player = gameObject.GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   


    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.pisado = true;
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        player.pisado = true;
        if (collision.transform.tag == "Puente" && player.GetCrouching() == true)
        {
            StartCoroutine(CambiarLayerPuente(collision));
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        player.pisado = false;
        
    }

    IEnumerator CambiarLayerPuente(Collider2D collision)
    {
        collision.gameObject.layer = 11;
        yield return new WaitForSeconds(2.5f);
        collision.gameObject.layer = 0;
    }


}
