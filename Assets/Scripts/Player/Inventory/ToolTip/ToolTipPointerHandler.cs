using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipPointerHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private ToolTip toolTip;
    private RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData) 
    {
        if (gameObject.GetComponent<Slot>().Items.Count > 0)
        {
            Debug.Log(gameObject.GetComponent<Slot>().Items[0]);
            var itemInSlot = gameObject.GetComponent<Slot>().Items[0].GetComponent<Item>();
            toolTip.DisplayInfo(itemInSlot.itemPrefab);
        }       
    }

    public void OnPointerExit(PointerEventData pointerEventData) 
    {
        toolTip.HideInfo();
    }
}
