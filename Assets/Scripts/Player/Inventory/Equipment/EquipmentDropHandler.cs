using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentDropHandler : MonoBehaviour, IDropHandler
{
    private EquipmentSlot equipmentSlot;
    [SerializeField]
    private Equipment equipment;

    private void Awake()
    {
        equipmentSlot = GetComponent<EquipmentSlot>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Slot draggedFromSlot = eventData.pointerDrag.GetComponentInParent<Slot>();
        if (draggedFromSlot.Items[0].GetComponent<EquipmentItem>() == null)
        {
            return;
        }
        GameObject equipmentItem = draggedFromSlot.Items[0];
        if (equipmentItem.GetComponent<EquipmentItem>().equipmentCategory == equipmentSlot.slotCategory)
        {
            if (equipmentSlot.Items.Count == 0)
            {
                AddItem(draggedFromSlot, equipmentItem);
            }
            else
            {
                SwapItem(draggedFromSlot, equipmentItem);
            }
        }
    }

    private void AddItem(Slot draggedFromSlot, GameObject equipmentItem) 
    {
        // Add items to new list
        equipmentSlot.Items = draggedFromSlot.Items.ToList();
        equipmentSlot.UpdateCountText();
        equipmentSlot.SetIcon(equipmentSlot.Items[0].GetComponent<Item>().icon);
        equipment.Equip(equipmentItem);

        // Remove items from old list
        draggedFromSlot.Items = new List<GameObject>();
        draggedFromSlot.UpdateCountText();
        draggedFromSlot.SetIcon(null);
    }

    private void SwapItem(Slot draggedFromSlot, GameObject equipmentItem) 
    {
        // Cache items from first list to put in the second list
        List<GameObject> itemsSecondList = new List<GameObject>();
        itemsSecondList = equipmentSlot.Items.ToList();

        // Add items to first list
        equipmentSlot.Items = draggedFromSlot.Items.ToList();
        equipmentSlot.UpdateCountText();
        equipmentSlot.SetIcon(equipmentSlot.Items[0].GetComponent<Item>().icon);

        // Set attributes
        equipment.Swap(equipmentItem, itemsSecondList[0]);

        // Add items to second list
        draggedFromSlot.Items = itemsSecondList.ToList();
        draggedFromSlot.UpdateCountText();      
        draggedFromSlot.SetIcon(draggedFromSlot.Items[0].GetComponent<Item>().icon);
        
        
    }
}
