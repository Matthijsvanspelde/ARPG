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

    public void Talk(GameObject player)
    {
        Debug.Log(currentDialogueNode.dialogue);
        foreach (var option in currentDialogueNode.dialogueOptions)
        {
            Debug.Log(option.optionText + " - " + option.nextDialogueNode.name);
        }
        SetSpriteDirection(player.transform.position.x);
    }

    public void SetCurrentDialogueNode() 
    { 
        
    }

    private void SetSpriteDirection(float playerXPosition)
    {
        float relativePosition = playerXPosition - transform.position.x;
        if (relativePosition > 0f)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else if (relativePosition < 0f)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
    }
}
