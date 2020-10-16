using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySecretLevel : MonoBehaviour
{
    public float speed = 2;

    public bool moveRigth = true;

    // Update is called once per frame
    void Update()
    {

        if (moveRigth == true)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

        }

        if (moveRigth == false)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);


        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemyStop")
        {
            moveRigth = false;
            transform.Rotate(0, -180, 0);
        }

        if (collision.gameObject.tag == "enemyStop1")
        {
            moveRigth = true;
            transform.Rotate(0, 180, 0);

        }
    }
  
}

