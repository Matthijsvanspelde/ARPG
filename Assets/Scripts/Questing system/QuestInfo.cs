using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class QuestInfo : ScriptableObject
{
    public bool isActive;
    public bool isCompleted;
    public string title;
    public string description;
    public int experienceReward;
    public int goldReward;
    public QuestGoal goal;
}
