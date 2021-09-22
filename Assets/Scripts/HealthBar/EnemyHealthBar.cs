using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : HealthBar
{
    public TMP_Text nameTag;
    public Image border;
    public Image fill;

    public void SetNameTag(string nameTag)
    {
        this.nameTag.text = nameTag;
    }

    public void SetVisibility(bool isVisible)
    {
        if (!isVisible)
        {
            nameTag.text = "";
            Color borderColor = border.color;
            borderColor.a = 0;
            Color fillColor = fill.color;
            fillColor.a = 0;
            border.color = borderColor;
            fill.color = fillColor;
        }
        else
        {
            Color borderColor = border.color;
            borderColor.a = 255;
            Color fillColor = fill.color;
            fillColor.a = 255;
            border.color = borderColor;
            fill.color = fillColor;
        }
        

    }

}
