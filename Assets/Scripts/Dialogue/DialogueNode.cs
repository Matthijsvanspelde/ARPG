using System.Collections.Generic;
using UnityEngine;

public class DialogueNode : MonoBehaviour
{
    [SerializeField]
    private string dialogue;
    [SerializeField]
    private List<DialogueOption> dialogueOptions;
}
