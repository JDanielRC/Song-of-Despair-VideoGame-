using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoss : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject boss;
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
            StartCoroutine(SpawnBoss());
        }
    }

    IEnumerator SpawnBoss()
    {
        if (boss != null)
        {
            yield return new WaitForSeconds(1.5f);
            boss.SetActive(true);
        }
        
    }

    IEnumerator OpenSecretRoom()
    {
        yield return new WaitForSeconds(6);
        //reproduce sonido mistico
        //abre sala secreta
    }
}
