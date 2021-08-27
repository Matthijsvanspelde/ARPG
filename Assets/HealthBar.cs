using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TMP_Text nameTag;

    private void Awake()
    {
        SetActive(false);
    }

    public void SetMaxHealth(int health) 
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health) 
    {
        slider.value = health;
    }

    public void SetNameTag(string nameTag) 
    {
        this.nameTag.text = nameTag;
    }

    public void SetActive(bool isActive) 
    {
        gameObject.SetActive(isActive);
    }
}
