using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text nameText;
    public Text dialogueText;
    //public Queue<string> sentences;

    string activeSentence;
    public float typingSpeed = 0.05f;

    AudioSource myAudio;
    public AudioClip speakSfx;

    [SerializeField] string[] sentences;
    [SerializeField] string[] sentencesUpdate = new string[5];
  

    //Clave del array de las frases
    int sentenceKey = 0;

    // Start is called before the first frame update
    void Start()
    {
        //sentences = new Queue<string>();
        //myAudio = GetComponent<AudioSource>();
    }
    private void Awake()
    {
        //dialogueBox.SetActive(false);
    }


    public void StartDialogue(Dialogues dialogues)
    {
        print("Empezamos");
        sentences = dialogues.sentences;
        dialogueBox.SetActive(true);

        sentenceKey = 0;
        //sentences.Clear();
        nameText.text = dialogues.charName;
        //dialogueText.text = sentences[sentenceKey];

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
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    /*
    public void StartDialogue (Dialogues dialogues)
    {
        //Debug.Log("Conv with" + dialogues.charName);
        dialogueBox.SetActive(true);        
        sentences.Clear();
        nameText.text = dialogues.charName;

        foreach (string sentence in dialogues.sentences)
        {           
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count < 0)
        {
            EndDialogue();
            return;
        }
        else
        {
            print("siguiente");
            activeSentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(activeSentence));
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

    void EndDialogue()
    {
        print("adios");
        dialogueBox.SetActive(false);
    }


    */

}
