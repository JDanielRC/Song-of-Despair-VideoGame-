using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Aliento());
    }

    // Update is called once per frame
    IEnumerator Aliento()
    {
        yield return new WaitForSeconds(.45f);
        Destroy(gameObject);
    }
}
