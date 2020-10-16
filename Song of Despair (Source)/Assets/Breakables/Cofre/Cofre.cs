using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cofre : MonoBehaviour
{
    // Start is called before the first frame update
    private bool hasBeenOpened;
    private CambioForma manager;
    private ReproductorExterno reproductor;
    private Animator anim;
    private Transform jugador;
    public GameObject[] lootList;
    void Start()
    {
        hasBeenOpened = false;
        manager = GameObject.FindGameObjectWithTag("ShapeController").GetComponent<CambioForma>();
        reproductor = GameObject.FindGameObjectWithTag("ShapeController").GetComponent<ReproductorExterno>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.GetCurrentForm() == 1)
        {
            GameObject jugadorX = GameObject.FindGameObjectWithTag("Player");
            if (jugadorX != null)
            {
                jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
                if (Input.GetKeyDown(KeyCode.F) && !hasBeenOpened && Vector2.Distance(jugador.position, transform.position) < 4)
                {
                    anim.SetTrigger("Open");
                    StartCoroutine(DropItems());
                    this.hasBeenOpened = true;
                }
            }
        }
    }

    IEnumerator DropItems()
    {
        reproductor.PlayOpenChestSound();
        yield return new WaitForSeconds(.09f);
        for (int i = 0; i < lootList.Length; i++)
        {
            GameObject newItem = Instantiate(lootList[i], transform.position, transform.rotation);
            newItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2.5f, 2.5f), Random.Range(2f, 5f)), ForceMode2D.Impulse);
        }
    }
}
