using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private int gold;
    [SerializeField]
    List<Slot> slots = new List<Slot>();
    [SerializeField]
    private GameObject inventoryPanel;
    private GameObject selectedItem = null;
    private bool isToggled;

    public bool IsToggled { get => isToggled; private set => isToggled = value; }

    void Start()
    {
        IsToggled = false;
        inventoryPanel.SetActive(false);
    }

    private void Update()
    {
        ToggleInventoryPanel();
    }


    private void ToggleInventoryPanel()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventoryPanel.activeSelf)
            {
                inventoryPanel.SetActive(true);
                IsToggled = true;
            }
            else
            {
                inventoryPanel.SetActive(false);
                IsToggled = false;
            }
        }
    }

    private void AddItem()
    {
        foreach (var slot in slots)
        {
            if (slot.Items.Count > 0 && slot.Items[0].name == selectedItem.name)
            {
                if (slot.Items[0].GetComponent<Item>().CanBeStacked && selectedItem.GetComponent<Item>().CanBeStacked)
                {
                    slot.Items.Add(selectedItem);
                    slot.UpdateCountText();
                    return;
                }
            }
        }
        foreach (var slot in slots)
        {
            if (slot.Items.Count == 0)
            {
                slot.Items.Add(selectedItem);
                slot.SetIcon(selectedItem.GetComponent<Item>().Icon);
                slot.UpdateCountText();
                return;
            }
        }
    }
}
