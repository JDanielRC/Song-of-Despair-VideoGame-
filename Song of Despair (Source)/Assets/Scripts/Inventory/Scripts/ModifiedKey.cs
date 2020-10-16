using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifiedKey : MonoBehaviour
{
    // Start is called before the first frame update
    public Key llave;
    public GameObject cerradura;

    void Start()
    {
        llave.GiveMeLock(cerradura);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
