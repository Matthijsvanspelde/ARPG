using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueNode", menuName = "Dialogue/DialogueNode", order = 1)]
public class DialogueNode : ScriptableObject
{
    [TextArea]
    public string dialogue;
    public DialogueOption option1;
    public DialogueOption option2;
    public DialogueOption option3;
}
