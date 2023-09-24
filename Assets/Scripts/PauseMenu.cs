using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPause = false;

    [SerializeField] public PlayerMain pmain;


    [SerializeField] public GameObject pauseMenuUI;
    [SerializeField] public GameObject controlMenuUI;
  
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }


    public void Resume()
    {
        controlMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 2f;
        GameIsPause = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Restart()
    {
        pmain.Morri();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 2f;
        GameIsPause = false;

        print("morre");
        print(pmain.morto);
    }

    public void ControlesLigar()
    {
        controlMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void SairControles()
    {
        pauseMenuUI.SetActive(true);
        controlMenuUI.SetActive(false);
    }
}
