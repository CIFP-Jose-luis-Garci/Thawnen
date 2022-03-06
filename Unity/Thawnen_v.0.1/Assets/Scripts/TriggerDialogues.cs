using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TriggerDialogues : MonoBehaviour
{
    [SerializeField] public GameObject dialogues;
    [SerializeField] public GameObject dialoguesbox;
    [SerializeField] public GameObject textbutton;
    [SerializeField] public Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        dialogues.SetActive(false);
        textbutton.SetActive(false);
        dialoguesbox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public   void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            textbutton.SetActive(true);
            dialogues.SetActive(true);
            textbutton.SetActive(true);
            //startButton.Select();
            EventSystem.current.SetSelectedGameObject(textbutton);
            


        }
        

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            textbutton.SetActive(false);
            dialogues.SetActive(false);
            textbutton.SetActive(false);
            //startButton.Select();
            EventSystem.current.SetSelectedGameObject(null); 



        }
    }


}
