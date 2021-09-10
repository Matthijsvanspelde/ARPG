using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerStatsScriptableObject stats;
    [SerializeField]
    private PlayerHealthBar healthBar;

    private void Awake()
    {
        healthBar.SetMaxHealth(stats.maxHealth);
        healthBar.SetHealth(stats.currentHealth);
    }

    public void TakeDamage(int damage) 
    {
        stats.currentHealth -= damage;
        healthBar.SetHealth(stats.currentHealth);
    }
}
