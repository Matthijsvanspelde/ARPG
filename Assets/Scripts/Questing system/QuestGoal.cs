using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;
    public EnemyType enemyType;
    public int requiredAmount;
    public int currentAmount;

    public bool isReached()    
    {
        return (currentAmount >= requiredAmount);
    }

    public void EnemyKilled(EnemyType enemyType) 
    {
        if (goalType == GoalType.kill && this.enemyType == enemyType)
        {
            currentAmount++;
        }
    }

    public void ItemGathered() 
    {
        if (goalType == GoalType.gathering)
        {
            currentAmount++;
        }
    }
}

public enum GoalType
{ 
    kill,
    gathering
}