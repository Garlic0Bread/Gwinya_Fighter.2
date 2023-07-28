using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    

    public void Changer()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Exit()
    {
        Application.Quit ();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
