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
        quest.questInfo = Instantiate(questInfoPrefab, questLogContent.transform);        
        UpdateQuestLog(quest);
    }

    public void UpdateQuestLog(Quest quest)
    {
        QuestInfoUI questInfoUI = quest.questInfo.GetComponent<QuestInfoUI>();
        questInfoUI.TitleText.text = quest.title;
        questInfoUI.DescriptionText.text = quest.description;
        questInfoUI.goldRewardText.text = quest.goldReward.ToString();
        questInfoUI.experienceRewardText.text = quest.experienceReward.ToString();
        questInfoUI.ProgressText.text = quest.goal.currentAmount + "/" + quest.goal.requiredAmount;
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
