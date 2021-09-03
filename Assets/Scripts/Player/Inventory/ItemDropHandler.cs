using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    private Slot slot;

    private void Awake()
    {
        slot = GetComponent<Slot>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Slot draggedFromSlot = eventData.pointerDrag.GetComponentInParent<Slot>();
        if (slot.Items.Count == 0)
        {
            // Add items to new list
            slot.Items = draggedFromSlot.Items.ToList();
            slot.UpdateCountText();
            slot.SetIcon(slot.Items[0].GetComponent<Item>().Icon);

            // Remove items from old list
            draggedFromSlot.Items = new List<GameObject>();
            draggedFromSlot.UpdateCountText();
            draggedFromSlot.SetIcon(null);

        }
        else if (slot.Items.Count > 0)
        {
            if (slot.Items[0].name == draggedFromSlot.Items[0].name && slot.Items[0].GetComponent<Item>().CanBeStacked && draggedFromSlot.Items[0].GetComponent<Item>().CanBeStacked) // Are the items from both slots the same item? Then stack them.
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
            else // Is the slot already occupied? Then swap items.
            {
                // Cache items from first list to put in the second list
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

            }
        }
        Debug.Log("Object: " + eventData.pointerDrag);
        Debug.Log("Dropped to: " + gameObject.name);
    }
}
