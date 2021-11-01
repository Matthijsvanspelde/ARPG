using TMPro;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public QuestInfo questInfo;
    public GameObject questInfoUI;
    public GameObject completePopup;
    [SerializeField]
    private TMP_Text headerText;
    [SerializeField]
    private TMP_Text titleText;
    [SerializeField]
    private AudioClip completeSound;

    public void Complete()
    {             
        headerText.text = "You have completed";
        titleText.text = questInfo.title;
        completePopup.GetComponent<Animator>().SetTrigger("LevelUp");
        AudioSource audioSource = completePopup.GetComponent<AudioSource>();
        audioSource.clip = completeSound;
        audioSource.Play();
        questInfo.isCompleted = true;
    }
}
