using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGame : MonoBehaviour
{

    [SerializeField] int currentScene;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.currentScene = currentScene;
    }

}
