using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentDropHandler : MonoBehaviour, IDropHandler
{
    private EquipmentSlot equipmentSlot;

    private void Awake()
    {
        equipmentSlot = GetComponent<EquipmentSlot>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Slot draggedFromSlot = eventData.pointerDrag.GetComponentInParent<Slot>();
        if (draggedFromSlot.Items[0].GetComponent<Item>().itemCategory == equipmentSlot.slotCategory)
        {
            if (equipmentSlot.Items.Count == 0)
            {
                // Add items to new list
                equipmentSlot.Items = draggedFromSlot.Items.ToList();
                equipmentSlot.UpdateCountText();
                equipmentSlot.SetIcon(equipmentSlot.Items[0].GetComponent<Item>().Icon);

                // Remove items from old list
                draggedFromSlot.Items = new List<GameObject>();
                draggedFromSlot.UpdateCountText();
                draggedFromSlot.SetIcon(null);
            }
            else
            {
                // Cache items from first list to put in the second list
                List<GameObject> itemsSecondList = new List<GameObject>();
                itemsSecondList = equipmentSlot.Items.ToList();

                // Add items to first list
                equipmentSlot.Items = draggedFromSlot.Items.ToList();
                equipmentSlot.UpdateCountText();
                equipmentSlot.SetIcon(equipmentSlot.Items[0].GetComponent<Item>().Icon);

                // Add items to second list
                draggedFromSlot.Items = itemsSecondList.ToList();
                draggedFromSlot.UpdateCountText();
                draggedFromSlot.SetIcon(draggedFromSlot.Items[0].GetComponent<Item>().Icon);
            }
        }
    }
}
