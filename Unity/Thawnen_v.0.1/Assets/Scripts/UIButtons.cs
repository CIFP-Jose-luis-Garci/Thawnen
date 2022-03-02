using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour
{
    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    //M�todo que carga la escena en la que est� el jugador jugando
    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(GameManager.currentScene);
    }
   public void doExitGame()
    {
        Application.Quit();
        print("he cerrado el juego");
    }
   
}
