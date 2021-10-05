using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private DialogueNode startingDialogueNode;
    private DialogueNode currentDialogueNode;   
    [SerializeField]
    private Transform rig;

    [Header("UI")]
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private TMP_Text dialogueText;
    [SerializeField]
    private TMP_Text nameTag;
    [SerializeField]
    private Button option1Button;
    [SerializeField]
    private Button option2Button;
    [SerializeField]
    private Button option3Button;

    private void Awake()
    {
        canvas.SetActive(false);
        currentDialogueNode = startingDialogueNode;
    }

    public void StartDialogue(GameObject player)
    {
        SetDialogueText();
        SetSpriteDirection(player.transform.position.x);
    }

    private void SetDialogueText() 
    {
        if (canvas != null)
        {
            canvas.SetActive(true);
            nameTag.text = gameObject.name;
            dialogueText.text = currentDialogueNode.dialogue;
            SetDialogueOptions();
        }
    }

    private void SetDialogueOptions() 
    {
        if (currentDialogueNode.option1 != null)
        {
            option1Button.GetComponentInChildren<TMP_Text>().text = currentDialogueNode.option1.optionText;
        }
        if (currentDialogueNode.option2 != null)
        {
            option2Button.GetComponentInChildren<TMP_Text>().text = currentDialogueNode.option2.optionText;
        }
        if (currentDialogueNode.option3 != null)
        {
            option3Button.GetComponentInChildren<TMP_Text>().text = currentDialogueNode.option3.optionText;
        }
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
