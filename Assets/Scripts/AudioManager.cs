using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager: MonoBehaviour
{

    public AudioSource backgroundMusic;

    private void Awake()
    {
            DontDestroyOnLoad(this.gameObject);
        
    }

    // Chame este m�todo para iniciar a m�sica de fundo.
    public void PlayBackgroundMusic()
    {
        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }
    }

    // Chame este m�todo para parar a m�sica de fundo.
    public void StopBackgroundMusic()
    {
        backgroundMusic.Stop();
    }

    // Chame este m�todo para pausar a m�sica de fundo.
    public void PauseBackgroundMusic()
    {
        backgroundMusic.Pause();
    }

    // Chame este m�todo para retomar a m�sica de fundo.
    public void ResumeBackgroundMusic()
    {
        backgroundMusic.UnPause();
    }

}
