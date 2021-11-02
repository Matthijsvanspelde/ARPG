using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestLog : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>();
    [SerializeField]
    private GameObject questLog;
    [SerializeField]
    private GameObject questLogContent;
    [SerializeField]
    private GameObject questInfoPrefab;
    private bool isOpen = false;

    private void Awake()
    {
        questLog.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleQuestLog();
        }
    }

    public void AddToQuestLog(Quest quest) 
    {
        quests.Add(quest);
        quest.questInfoUI = Instantiate(questInfoPrefab, questLogContent.transform);        
        UpdateQuestLog(quest);
    }

    public void UpdateQuestLog(Quest quest)
    {       
        QuestInfoUI questInfoUI = quest.questInfoUI.GetComponent<QuestInfoUI>();
        if (quest.questInfo.isCompleted)
        {
            Destroy(questInfoUI.gameObject);
        }
        else
        {
            questInfoUI.TitleText.text = quest.questInfo.title;
            questInfoUI.DescriptionText.text = quest.questInfo.description;
            questInfoUI.goldRewardText.text = quest.questInfo.goldReward + " Gold";
            questInfoUI.experienceRewardText.text = quest.questInfo.experienceReward + " XP";
            questInfoUI.ProgressText.text = quest.questInfo.goal.currentAmount + "/" + quest.questInfo.goal.requiredAmount + " " + quest.questInfo.goal.enemyType + "(s)";
        }        
    }

    public void ToggleQuestLog()
    {
        if (!isOpen)
        {
            questLog.SetActive(true);
            isOpen = true;
        }
        else
        {
            questLog.SetActive(false);
            isOpen = false;
        }
    }

    
}
