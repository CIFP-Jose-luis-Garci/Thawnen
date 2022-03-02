using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text nameText;
    public Text dialogueText;
    

    string activeSentence;
    public float typingSpeed = 0.005f;

    AudioSource myAudio;
    public AudioClip speakSfx;

    [SerializeField] string[] sentences;
    [SerializeField] string[] sentencesUpdate = new string[5];
  

    //Clave del array de las frases
    int sentenceKey = 0;

    // Start is called before the first frame update
    void Start()
    {        
        //myAudio = GetComponent<AudioSource>();
    }
   

    public void StartDialogue(Dialogues dialogues)
    {
        print("Empezamos");
        sentences = dialogues.sentences;
        dialogueBox.SetActive(true);

        sentenceKey = 0;        
        nameText.text = dialogues.charName;

        StartCoroutine("TypeSentence", sentences[sentenceKey]);
    }

    public void DisplayNextSentence()
    {        
        //StopCoroutine("TypeSentence");
        sentenceKey++;
        //Si hemos llegado al final del array
        if(sentenceKey == sentences.Length)
        {
            dialogueBox.SetActive(false);

        }
        else
        {
            //dialogueText.text = sentences[sentenceKey];
            //Iniciamos la corrutina que muestra el texto
            StartCoroutine("TypeSentence", sentences[sentenceKey]);
        }
       
        

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return typingSpeed;
        }
    }

   
}
