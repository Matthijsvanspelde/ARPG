using TMPro;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public PlayerStatsScriptableObject playerData;
    private GameObject player;
    
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text experienceText;
    public TMP_Text goldText;

    public GameObject questWindow;
    public void OpenQuestWindow(GameObject player) 
    {
        questWindow.SetActive(true);
        this.player = player;
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        experienceText.text = quest.experienceReward.ToString();
        goldText.text = quest.goldReward.ToString();
    }

    public void CloseQuestWindow()
    {
        questWindow.SetActive(false);
    }

    public void AcceptQuest() 
    {
        questWindow.SetActive(false);
        quest.isActive = true;
        player.GetComponent<QuestLog>().quests.Add(quest);
    }
}
