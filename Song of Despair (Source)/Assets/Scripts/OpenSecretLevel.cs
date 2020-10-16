using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSecretLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject boss;
    public GameObject puerta;
    private bool executed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (boss.gameObject == null && !executed)
        {
            print("c murio el boss");
            executed = true;
            StartCoroutine(AbrePuerta());

        }
    }

    IEnumerator AbrePuerta()
    {
        GameObject.FindGameObjectWithTag("MusicaFondo").GetComponent<MusicaFondo>().PlayPreBoss();
        yield return new WaitForSeconds(1);
        GameObject.FindGameObjectWithTag("Reproductor2").GetComponent<AudioDos>().PlayOpenSound();
        yield return new WaitForSeconds(5);
        puerta.SetActive(true);
    }
}
