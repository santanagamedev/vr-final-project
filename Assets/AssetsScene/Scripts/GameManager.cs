using System.Collections;
using System.Collections.Generic;
using Meta.XR.InputActions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class GameManager : MonoBehaviour
{
    public GameObject pausaUI;
    public bool activeUI = true;

    private void Start() 
    {
        DisplayUI();
    } 

    //Funcion boton de pausa
    public void PresionarBotonPausa(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            DisplayUI();
        }
    }

    //Activar menu de pausa
    public void DisplayUI()
    {
        if (activeUI)
        {
            pausaUI.SetActive(false);
            activeUI = false;
            Time.timeScale = 1;
        }
        else if (!activeUI)
        {
            pausaUI.SetActive(true);
            activeUI = true;
            Time.timeScale = 0;
        }
    }
    //Cambio de escena menu a principal
    public void Jugar()
    {
        SceneManager.LoadScene(1);
    }

    //Reiniciar el juego
    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Salir de la aplicacion
    public void Salir()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
