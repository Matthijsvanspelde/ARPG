using System.Collections.Generic;
using UnityEngine;

public class QuestLog : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>();
    [SerializeField]
    private GameObject questLog;
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
