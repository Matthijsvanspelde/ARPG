using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventoryData inventoryData;
    [SerializeField]
    private List<Slot> slots = new List<Slot>();
    [SerializeField]
    private GameObject inventoryPanel;
    private bool isToggled;   
    [SerializeField]
    private TMP_Text goldText;
    public bool IsToggled { get => isToggled; private set => isToggled = value; }

    private void Awake()
    {
        IsToggled = false;
        goldText.text = inventoryData.gold.ToString();
    }

    void Start()
    {        
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

    public void AddItem(GameObject selectedItem)
    {
        foreach (var slot in slots)
        {
            if (slot.Items.Count > 0 && slot.Items[0].name == selectedItem.name)
            {
                if (slot.Items[0].GetComponent<Item>().stackSize > slot.Items.Count && selectedItem.GetComponent<Item>().stackSize > slot.Items.Count)
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
                slot.SetIcon(selectedItem.GetComponent<Item>().icon);
                slot.UpdateCountText();
                return;
            }
        }
    }

    public void UpdateGoldText() 
    {
        goldText.text = inventoryData.gold.ToString();
    }
}
