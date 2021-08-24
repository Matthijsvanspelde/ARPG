using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerStatsScriptableObject", order = 1)]
public class PlayerStatsScriptableObject : ScriptableObject
{   
    public int health = 20;
    public int mana = 10;
    public int stamina = 10;
    public float attackSpeed = 2f;
    public int baseAttackDamage = 4;
}
