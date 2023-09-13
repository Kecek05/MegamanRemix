using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadBehaviour : MonoBehaviour
{
    public Slider slider;
    AsyncOperation operation;
    static string level;
    void Start()
    {
        operation = SceneManager.LoadSceneAsync(level);
    }
    private void Update()
    {
        slider.value = operation.progress;
    }

    // Statica nao precisa estar na cena e é igual para todos os objetos com o codigo. Se o inimigo tem a variavel vida Static, quando ele receber dano, todos os inimigos recebem dano
    // Call This to load SceneManager
    public static void LoadLevel(string nextLevel)
    {
        level = nextLevel;
        SceneManager.LoadScene("Loading");

    }


    //private IEnumerator LoadDelay()
    //{


    //    yield return new WaitForSeconds(1f);

    //}
}
