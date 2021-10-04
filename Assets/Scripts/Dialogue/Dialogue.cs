using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private DialogueNode startingDialogueNode;
    private DialogueNode currentDialogueNode;
    [SerializeField]
    private TMP_Text dialogueText;
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private TMP_Text nameTag;
    [SerializeField]
    private Transform rig;

    private void Awake()
    {
        canvas.SetActive(false);
        currentDialogueNode = startingDialogueNode;
    }

    public void StartDialogue(GameObject player)
    {
        if (canvas != null)
        {
            canvas.SetActive(true);
            nameTag.text = gameObject.name;
            dialogueText.text = currentDialogueNode.dialogue;
            foreach (var option in currentDialogueNode.dialogueOptions)
            {
                Debug.Log(option.optionText + " - " + option.nextDialogueNode.name);
            }            
        }
        SetSpriteDirection(player.transform.position.x);
    }

    public void EndDialogue() 
    {
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
    }

    public void SetCurrentDialogueNode() 
    { 
        
    }

    private void SetSpriteDirection(float playerXPosition)
    {
        float relativePosition = playerXPosition - transform.position.x;
        if (relativePosition > 0f)
        {
            rig.localScale = new Vector3(1, rig.localScale.y, rig.localScale.z);
        }
        else if (relativePosition < 0f)
        {
            rig.localScale = new Vector3(-1, rig.localScale.y, rig.localScale.z);
        }
    }
}
