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

    private AudioSource audioSource;
    [SerializeField] private AudioClip acceptSound;

    private void Awake()
    {
        if (!quest.questInfo.isActive)
        {
            exclamationMark.SetActive(true);
        }
        else
        {
            exclamationMark.SetActive(false);
        }      
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OpenQuestWindow(GameObject player) 
    {
        SetSpriteDirection(player.transform.position.x);
        if (!quest.questInfo.isActive && !quest.questInfo.isCompleted)
        {
            questWindow.SetActive(true);
            this.player = player;
            titleText.text = quest.questInfo.title;
            descriptionText.text = quest.questInfo.description;
            rewardText.text = quest.questInfo.experienceReward + " experience, " + quest.questInfo.goldReward + " gold";
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
        quest.questInfo.isActive = true;
        player.GetComponent<QuestLog>().AddToQuestLog(quest);
        audioSource.clip = acceptSound;
        audioSource.Play();
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
