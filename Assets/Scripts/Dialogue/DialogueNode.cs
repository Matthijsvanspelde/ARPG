using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueNode", menuName = "Dialogue/DialogueNode", order = 1)]
public class DialogueNode : ScriptableObject
{
    [TextArea]
    public string dialogue;
    public List<DialogueOption> dialogueOptions;
}
