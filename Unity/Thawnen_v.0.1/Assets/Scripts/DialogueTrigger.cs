using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogues dialogues;
    public DialogueManager dialogueManager;
    GameObject dialogueBox;

    private TriggerDialogues triggerdiologuesardilla;
    private TriggerDialogues triggerdialoguesoso;
    private void Start()
    {
        triggerdiologuesardilla = GameObject.Find("ardilla").GetComponent<TriggerDialogues>();
        triggerdialoguesoso = GameObject.Find("bjorno").GetComponent<TriggerDialogues>();
    }
    public void TriggerDialogue()
    {
        //FindObjectOfType<DialogueManager>().StartDialogue(dialogues);
        dialogueManager.StartDialogue(dialogues);
        triggerdiologuesardilla.textbutton.SetActive(false);
        triggerdialoguesoso.textbutton.SetActive(false);
        //dialogueBox.SetActive(true);
        //Debug.Log("Te hablo");
    }
}
