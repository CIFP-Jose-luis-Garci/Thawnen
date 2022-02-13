using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(2);
        LoadingData.sceneToLoad = scene;
    }
   public void doExitGame()
    {
        Application.Quit();
        print("he cerrado el juego");
    }
}
