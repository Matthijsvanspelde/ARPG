using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageNumberUI : MonoBehaviour
{
    private TMP_Text damageText;

    private void Awake()
    {
        damageText = GetComponent<TMP_Text>();
    }

    public void SetValue(float value) 
    {
        damageText.text = value.ToString();
    }
}
