using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerStatsScriptableObject", order = 1)]
public class PlayerStatsScriptableObject : ScriptableObject
{   
    public float currentHealth = 20;
    public float maxHealth = 20;
    public int level = 1;
    public int currentExperience = 0;
    public int maxExperience = 100;
    public float attackSpeed = 2f;
    public float attackDamage = 4;
    public int strength = 1;
    public int dexterity = 1;
    public int armor = 1;
}
