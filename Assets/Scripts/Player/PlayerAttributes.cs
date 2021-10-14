using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public PlayerStatsScriptableObject attributes;
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
    private TMP_Text attackRangeText;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private TMP_Text levelPopupText;

    private float maxHealthBonus;
    private int armorBonus;
    private float attackSpeedBonus;
    private float attackDamageBonus;
    private float attackRangeBonus;
    private int strengthBonus;
    private int dexterityBonus;

    public float MaxHealthBonus { get => maxHealthBonus; private set => maxHealthBonus = value; }
    public int ArmorBonus { get => armorBonus; private set => armorBonus = value; }
    public float AttackSpeedBonus { get => attackSpeedBonus; private set => attackSpeedBonus = value; }
    public float AttackDamageBonus { get => attackDamageBonus; private set => attackDamageBonus = value; }
    public int StrengthBonus { get => strengthBonus; private set => strengthBonus = value; }
    public int DexterityBonus { get => dexterityBonus; private set => dexterityBonus = value; }
    public float AttackRangeBonus { get => attackRangeBonus; private set => attackRangeBonus = value; }

    private void Awake()
    {
        SetAttributeText();
        experienceBar.SetMaxValue(attributes.maxExperience);
        experienceBar.SetCurrentValue(attributes.currentExperience);
        healthBar.SetMaxValue(attributes.maxHealth);
        healthBar.SetCurrentValue(attributes.currentHealth);
    }

    public void TakeDamage(float damage) 
    {
        attributes.currentHealth -= damage - ((attributes.armor + armorBonus) / 100);
        healthBar.SetCurrentValue(attributes.currentHealth);
    }

    public void EarnExperience(int experience) 
    {
        attributes.currentExperience += experience;       
        if (attributes.currentExperience >= attributes.maxExperience)
        {
            LevelUp();
        }
        experienceBar.SetCurrentValue(attributes.currentExperience);
    }

    public void SetAttributeText() 
    {
        levelText.text = attributes.level.ToString();
        healthText.text = attributes.currentHealth + "/" + (attributes.maxHealth + maxHealthBonus);
        strengthText.text = (attributes.strength + strengthBonus).ToString();
        dexterityText.text = (attributes.dexterity + dexterityBonus).ToString();
        armorText.text = (attributes.armor + armorBonus).ToString();
        attackDamageText.text = (attributes.attackDamage + attackDamageBonus + ((float)(attributes.strength + strengthBonus) / 100)).ToString();
        attackSpeedText.text = (attributes.attackSpeed + AttackSpeedBonus + (((float)attributes.dexterity + dexterityBonus) / 100)).ToString();
        attackRangeText.text = (attributes.attackRange + attackRangeBonus).ToString();
    }

    private void LevelUp() 
    {
        // Set attributes
        attributes.level++;
        attributes.strength++;
        attributes.dexterity++;
        attributes.maxHealth += 10 * attributes.level;
        attributes.currentHealth = attributes.maxHealth;

        // Set experience bar
        attributes.currentExperience = attributes.currentExperience - attributes.maxExperience;
        attributes.maxExperience *= 2;

        // Set UI
        experienceBar.SetMaxValue(attributes.maxExperience);        
        healthBar.SetMaxValue(attributes.maxHealth);
        healthBar.SetCurrentValue(attributes.currentHealth);
        SetAttributeText();
        levelPopupText.text = "Level " + attributes.level;
        animator.SetTrigger("LevelUp");
    }

    public void AddAttributes(GameObject item) 
    {
        EquipmentItem equipmentItem = item.GetComponent<EquipmentItem>();
        maxHealthBonus += equipmentItem.health;
        armorBonus += equipmentItem.armor;
        strengthBonus += equipmentItem.strength;
        dexterityBonus += equipmentItem.dexterity;

        if (equipmentItem.equipmentCategory == EquimentSlotEnum.Weapon && item.GetComponent<WeaponItem>() != null)
        {
            WeaponItem weaponItem = item.GetComponent<WeaponItem>();
            attackDamageBonus += weaponItem.attackDamage;
            attackSpeedBonus += weaponItem.attackSpeed;
            attackRangeBonus += weaponItem.attackRange;
        }
        SetAttributeText();
    }

    public void RemoveAttributes(GameObject item) 
    {
        EquipmentItem equipmentItem = item.GetComponent<EquipmentItem>();
        maxHealthBonus -= equipmentItem.health;
        armorBonus -= equipmentItem.armor;
        strengthBonus -= equipmentItem.strength;
        dexterityBonus -= equipmentItem.dexterity;

        if (equipmentItem.equipmentCategory == EquimentSlotEnum.Weapon && item.GetComponent<WeaponItem>() != null)
        {
            WeaponItem weaponItem = item.GetComponent<WeaponItem>();
            attackDamageBonus -= weaponItem.attackDamage;
            attackSpeedBonus -= weaponItem.attackSpeed;
            attackRangeBonus -= weaponItem.attackRange;
        }
        SetAttributeText();
    }
}
