using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerStatsScriptableObject", order = 1)]
public class PlayerStatsScriptableObject : ScriptableObject
{   
    public int currentHealth = 20;
    public int maxHealth = 20;
    public float attackSpeed = 2f;
    public int baseAttackDamage = 4;
    public int strength = 1;
    public int armor = 1;
}
