using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{ 

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Debug.Log("YA ME SALI");
        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        int shape = GameObject.FindGameObjectWithTag("ShapeController").GetComponent<CambioForma>().GetCurrentForm();
        if (shape == 1)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Reaparecer();
        } else if (shape == 2)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<MovimientoParrot>().Reaparecer();
        }
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Win()
    {
        SceneManager.LoadScene(0);
    }

}
