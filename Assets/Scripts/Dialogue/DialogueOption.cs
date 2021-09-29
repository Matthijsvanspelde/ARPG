using UnityEngine;

[CreateAssetMenu(fileName = "DialogueOption", menuName = "Dialogue/DialogueOption", order = 2)]
public class DialogueOption : ScriptableObject
{
    public string optionText;
    public DialogueNode nextDialogueNode;
}
