using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerStatsScriptableObject stats;
    [SerializeField]
    private PlayerHealthBar healthBar;
    [SerializeField]
    private ExperienceBar experienceBar;
    [SerializeField]
    private TMP_Text levelText;
    [SerializeField]
    private TMP_Text healthText;
    [SerializeField]
    private TMP_Text strengthText;
    [SerializeField]
    private TMP_Text dexterityText;
    [SerializeField]
    private TMP_Text armorText;
    [SerializeField]
    private TMP_Text attackDamageText;
    [SerializeField]
    private TMP_Text attackSpeedText;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private TMP_Text levelPopupText;

    private void Awake()
    {
        SetAttributeText();
        experienceBar.SetMaxValue(stats.maxExperience);
        experienceBar.SetCurrentValue(stats.currentExperience);
        healthBar.SetMaxValue(stats.maxHealth);
        healthBar.SetCurrentValue(stats.currentHealth);
    }

    public void TakeDamage(float damage) 
    {
        stats.currentHealth -= damage;
        healthBar.SetCurrentValue(stats.currentHealth);
    }

    public void EarnExperience(int experience) 
    {
        stats.currentExperience += experience;       
        if (stats.currentExperience >= stats.maxExperience)
        {
            LevelUp();
        }
        experienceBar.SetCurrentValue(stats.currentExperience);
    }

    public void SetAttributeText() 
    {
        levelText.text = stats.level.ToString();
        healthText.text = stats.maxHealth.ToString();
        strengthText.text = stats.strength.ToString();
        dexterityText.text = stats.dexterity.ToString();
        armorText.text = stats.armor.ToString();
        attackDamageText.text = (stats.baseAttackDamage + ((float)stats.strength / 100)).ToString();
        attackSpeedText.text = (stats.attackSpeed + ((float)stats.dexterity / 100)).ToString();
    }

    private void LevelUp() 
    {
        // Set attributes
        stats.level++;
        stats.strength++;
        stats.dexterity++;
        stats.maxHealth += 10 * stats.level;
        stats.currentHealth = stats.maxHealth;

        // Set experience bar
        stats.currentExperience = stats.currentExperience - stats.maxExperience;
        stats.maxExperience *= 2;

        // Set UI
        experienceBar.SetMaxValue(stats.maxExperience);        
        healthBar.SetMaxValue(stats.maxHealth);
        healthBar.SetCurrentValue(stats.currentHealth);
        SetAttributeText();
        levelPopupText.text = "Level " + stats.level;
        animator.SetTrigger("LevelUp");
    }
}
