using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialoguesSec : MonoBehaviour
{
    public string charName;

    [TextArea(1, 3)]
    public string[] sentences;
        
}
