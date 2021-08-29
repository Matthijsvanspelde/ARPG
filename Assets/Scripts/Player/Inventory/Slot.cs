using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> items = new List<GameObject>();
    [SerializeField]
    private TMP_Text itemCountText;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private Sprite defaultIcon;

    public List<GameObject> Items { get => items; set => items = value; }


    public void UpdateCountText()
    {

        itemCountText.text = items.Count.ToString();
    }

    public void SetIcon(Sprite itemIcon)
    {
        if (itemIcon != null)
        {
            icon.sprite = itemIcon;
        }
        else
        {
            icon.sprite = defaultIcon;
        }
    }
}
