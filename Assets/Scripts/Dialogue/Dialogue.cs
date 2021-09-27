using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private DialogueNode startingDialogueNode;
    private DialogueNode currentDialogueNode;

    private void Awake()
    {
        currentDialogueNode = startingDialogueNode;
    }

    private void SetCurrentDialogueNode() 
    { 
        
    }
}
