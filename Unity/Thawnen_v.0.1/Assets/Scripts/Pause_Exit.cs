using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;// Required when using Event data.

public class Pause_Exit : MonoBehaviour
{

    //Instancia del Input System
    InputActions inputActions;

    //Obtenemos el menu que queremos desactivar/activar
    //Es un Empty Object que actua como padre de los elementos
    [SerializeField] GameObject resumeMenu;

    //Texto del contador que nos permite ver cómo se pausa el juego
    //[SerializeField] Text contador;
    int contadorNum = 0;

    //Booleana que nos dice si el juego está pausado o no
    //bool gamePaused = false;

    //Botones del menú
    [SerializeField] Button btnResume, btnQuit;

    private void Awake()
    {
        inputActions = new InputActions();

        //Botón de pausa
        inputActions.Pause.Pause.performed += _ => ActivarMenu();

    }
    // Start is called before the first frame update
    void Start()
    {
        //Obtenemos el menú mediante código (en vez de arrastrarlo)
        //IMPORTANTE: Cambiar el nombre del gameobject por el vuestro
        resumeMenu = GameObject.Find("MenuPause");

        //Lo desactivamos de inicio
        resumeMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //Método que ejecutamos constantemente
        // Podemos cambiarlo por ejemplo por el movimiento de nuestro personaje
        Contador();


    }


    void ActivarMenu()
    {
        //Interruptor que permite activar y desactivar un menú
        //Si no está pausado, lo pausamos
        if (!GameManager.gamePaused)
        {
            GameManager.gamePaused = true;

            //Detenemos el tiempo del juego
            Time.timeScale = 0f;
        }
        else
        {
            GameManager.gamePaused = false;
            //Devolvemos el paso del tiempo al juego
            Time.timeScale = 1f;
        }
        //Lo que haya salido de este interruptor, lo pasamos al menú para que se active o no
        resumeMenu.SetActive(GameManager.gamePaused);
        if (GameManager.gamePaused)
            btnResume.Select();
    }

    void Contador()
    {

        //Si el juego está pausado, detenemos el método mediante un return
        //NOTA: cuando un IF solo tiene una línea, no es necesario llaves
        if (GameManager.gamePaused)
            return;

        //Vamos aumentando el contador
        contadorNum++;
        //contador.text = contadorNum.ToString();

    }

    //FUNCIONES PARA LOS BOTONES
    public void Resume()
    {
        //DESpausamos el juego y ocultamos el menú
        GameManager.gamePaused = false;
        Time.timeScale = 1f;
        resumeMenu.SetActive(false);

    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void LoadScene(int escena)
    {

    }
}