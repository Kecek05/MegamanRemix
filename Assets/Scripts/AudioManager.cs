using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager: MonoBehaviour
{

    public AudioSource backgroundMusic;

    private void Awake()
    {
            DontDestroyOnLoad(this.gameObject);
        
    }
    private void Update()
    {

        string nomeDaCena = SceneManager.GetActiveScene().name;
        if (nomeDaCena == "GameOver" || nomeDaCena == "Menu")
        {
            PauseBackgroundMusic();
        }
        else
        {
            ResumeBackgroundMusic();
        }

    }

    // Chame este método para iniciar a música de fundo.
    public void PlayBackgroundMusic()
    {
        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }
    }

    // Chame este método para parar a música de fundo.
    public void StopBackgroundMusic()
    {
        backgroundMusic.Stop();
    }

    // Chame este método para pausar a música de fundo.
    public void PauseBackgroundMusic()
    {
        backgroundMusic.Pause();
    }

    // Chame este método para retomar a música de fundo.
    public void ResumeBackgroundMusic()
    {
        backgroundMusic.UnPause();
    }

}
