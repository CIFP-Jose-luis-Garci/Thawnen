using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Button controlsbutton;
    [SerializeField] Button continueButton;
    // Start is called before the first frame update
    void Start()
    {
        optionsMenu.SetActive(false);
    }

    public void OptionsMenu()
    {
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        controlsbutton.Select();

    }
    public void OptionsMenuOcult()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        continueButton.Select();

    }
}
