using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    private Slot slot;
    [SerializeField]
    private Equipment equipment;

    private void Awake()
    {
        slot = GetComponent<Slot>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Slot draggedFromSlot = eventData.pointerDrag.GetComponentInParent<Slot>();
        if (slot.Items.Count == 0)
        {
            AddItem(draggedFromSlot);           
        }
        else if (slot.Items.Count > 0)
        {
            if (slot.Items[0].name == draggedFromSlot.Items[0].name 
                && slot.Items.Count < slot.Items[0].GetComponent<Item>().StackSize 
                && draggedFromSlot.Items.Count < draggedFromSlot.Items[0].GetComponent<Item>().StackSize
                && !slot.Items[0] == draggedFromSlot.Items[0])
            {
                StackItem(draggedFromSlot);
            }
            else
            {
                SwapItem(draggedFromSlot);
            }
        }
    }

    private void AddItem(Slot draggedFromSlot) 
    {
        // Add items to new list
        slot.Items = draggedFromSlot.Items.ToList();
        slot.UpdateCountText();
        slot.SetIcon(slot.Items[0].GetComponent<Item>().Icon);

        // Change sprite
        if (draggedFromSlot.Items[0].GetComponent<EquipmentItem>() != null)
        {
            equipment.SetDefaultSprite(draggedFromSlot.Items[0].GetComponent<EquipmentItem>().itemCategory);
        }

        // Remove items from old list
        draggedFromSlot.Items = new List<GameObject>();
        draggedFromSlot.UpdateCountText();
        draggedFromSlot.SetIcon(null);
    }

    private void StackItem(Slot draggedFromSlot) 
    {
        foreach (var item in draggedFromSlot.Items.ToList())
        {
            // Add the items to new list
            slot.Items.Add(item);
            slot.UpdateCountText();

            // Remove items from old list
            draggedFromSlot.Items = new List<GameObject>();
            draggedFromSlot.UpdateCountText();
            draggedFromSlot.SetIcon(null);
        }
    }

    private void SwapItem(Slot draggedFromSlot) 
    {
        // Save items from first list to put in the second list
        List<GameObject> itemsSecondList = new List<GameObject>();
        itemsSecondList = slot.Items.ToList();

        // Add items to first list
        slot.Items = draggedFromSlot.Items.ToList();
        slot.UpdateCountText();
        slot.SetIcon(slot.Items[0].GetComponent<Item>().Icon);

        // Add items to second list
        draggedFromSlot.Items = itemsSecondList.ToList();
        draggedFromSlot.UpdateCountText();
        draggedFromSlot.SetIcon(draggedFromSlot.Items[0].GetComponent<Item>().Icon);

        // Change sprite
        if (draggedFromSlot.Items[0].GetComponent<EquipmentItem>() != null && draggedFromSlot.GetComponent<EquipmentSlot>() != null)
        {
            equipment.SetArmorSprite(draggedFromSlot.Items[0].GetComponent<EquipmentItem>());
        }
    }
}
