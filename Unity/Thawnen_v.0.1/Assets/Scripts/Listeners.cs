using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;// Required when using Event data.


public class Listeners : MonoBehaviour, ISelectHandler
{
    //Empty que contiene los sliders de sonido
    GameObject ControlsMenu;
    GameObject SoundMenu;

    private void Awake()
    {
        ControlsMenu = GameObject.Find("ControlsMenu");
        SoundMenu = GameObject.Find("SoundMenu");
    }
    //Listeners
    public void OnSelect(BaseEventData eventData)
    {
        string nombreBtn = this.gameObject.name;
        Debug.Log("Se ha seleccionado el boton: " + nombreBtn);
        //Dependiendo del nombre, o el tag, podemos tomar decisiones
        if (nombreBtn == "Controls")
        {
            //Accedemos al objeto que contiene los sliders de volumen y lo desactivamos

            ControlsMenu.SetActive(true);
        }
        else
        {
            ControlsMenu.SetActive(false);
        }

        if (nombreBtn == "Sound")
        {
            //Accedemos al objeto que contiene los sliders de volumen y lo desactivamos

            SoundMenu.SetActive(true);
        }
        else
        {
            SoundMenu.SetActive(false);
        }

    }
}