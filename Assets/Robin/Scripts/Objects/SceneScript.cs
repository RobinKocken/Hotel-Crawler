using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLobby()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
