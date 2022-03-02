using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text nameText;
    public Text dialogueText;
    //public Queue<string> sentences;

    string activeSentence;
    public float typingSpeed = 1f;

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
        dialogueText.text = sentences[sentenceKey];

        sentencesUpdate = new string[sentences.Length];
        for (int n = 0; n < sentences.Length; n++)
        {
            sentencesUpdate[n] = sentences[n];
            
        }
       
        //print(sentences[1]);

    }

    public void DisplayNextSentence()
    {
        sentenceKey++;
        if(sentenceKey == sentencesUpdate.Length)
        {
            dialogueBox.SetActive(false);

        }
        else
        {
            dialogueText.text = sentencesUpdate[sentenceKey];
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
