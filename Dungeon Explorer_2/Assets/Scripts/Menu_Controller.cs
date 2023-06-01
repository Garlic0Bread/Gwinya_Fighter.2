using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Controller : MonoBehaviour
{
    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void Game_Start()
    {
        SceneManager.LoadScene(1);
    }

    public void Main_Menu()
    {
        SceneManager.LoadScene(0);
        
    }

    public void How_Play()
    {
        SceneManager.LoadScene(2);
    }

    public void Credit()
    {
        SceneManager.LoadScene(3);
    }

    public void End_Game()
    {
        Application.Quit();
    }
}
