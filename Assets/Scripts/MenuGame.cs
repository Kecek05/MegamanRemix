using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGame : MonoBehaviour
{

    private void Update()
    {
        Cursor.visible = true;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        LoadBehaviour.LoadLevel("Level2");
     //   SceneManager.LoadScene("Level2");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ControlesMenu()
    {
        SceneManager.LoadScene("Controles");
    }
}
