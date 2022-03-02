using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogues dialogues;
    public DialogueManager dialogueManager;
    GameObject dialogueBox;

    public void TriggerDialogue()
    {
        //FindObjectOfType<DialogueManager>().StartDialogue(dialogues);
        dialogueManager.StartDialogue(dialogues);
        //dialogueBox.SetActive(true);
        //Debug.Log("Te hablo");
    }
}
