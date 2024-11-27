using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool isPaused;
    public Button salirButton;
    void Start()
    {
        isPaused = false;

        if (salirButton != null)
        {
            // Asocia el evento de clic del botón con la función Salir
            salirButton.onClick.AddListener(SalirDeLaAplicacion);
        }
    }

    //Cambio de escena menu a principal
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Pausar el juego
    public void PauseGame()
    {
        isPaused = !isPaused;
        if(isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    //Reiniciar el juego
    public void Reiniciar ()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Salir de la aplicacion
    void SalirDeLaAplicacion()
    {

        Debug.Log("Saliendo de la aplicación");
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
