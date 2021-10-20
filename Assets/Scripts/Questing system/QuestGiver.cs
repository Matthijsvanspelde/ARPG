using TMPro;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public PlayerStatsScriptableObject playerData;
    private GameObject player;
    
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text rewardText;

    public GameObject questWindow;
    public GameObject exclamationMark;

    private void Awake()
    {
        exclamationMark.SetActive(true);
    }

    public void OpenQuestWindow(GameObject player) 
    {
        SetSpriteDirection(player.transform.position.x);
        if (!quest.isActive)
        {
            questWindow.SetActive(true);
            this.player = player;
            titleText.text = quest.title;
            descriptionText.text = quest.description;
            rewardText.text = quest.experienceReward + " experience, " + quest.goldReward + " gold";
        }       
    }

    public void CloseQuestWindow()
    {
        questWindow.SetActive(false);
    }

    public void AcceptQuest() 
    {
        questWindow.SetActive(false);
        exclamationMark.SetActive(false);
        quest.isActive = true;
        player.GetComponent<QuestLog>().AddToQuestLog(quest);
    }

    private void SetSpriteDirection(float playerXPosition)
    {
        float relativePosition = playerXPosition - transform.position.x;
        if (relativePosition > 0f)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else if (relativePosition < 0f)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
    }
}
