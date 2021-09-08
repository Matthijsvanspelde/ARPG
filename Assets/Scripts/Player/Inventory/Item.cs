using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    public int stackSize;
    [SerializeField]
    private Sprite icon;
    public ItemEnum itemCategory;
    [Range(0, 100)]
    public int dropChance;

    [Header("Clone")]
    public ItemScriptableObject itemPrefab;

    [Header("Name Tag")]
    [SerializeField]
    private TMP_Text itemName;
    public GameObject canvas;
    
    public int StackSize { get => stackSize; private set => stackSize = value; }
    public Sprite Icon { get => icon; private set => icon = value; }
    public ItemEnum ItemCategory { get => itemCategory; private set => itemCategory = value; }

    private void Awake()
    {
        itemName.text = gameObject.name;
    }

    private void Start()
    {
        canvas.SetActive(false);
    }

    private void OnMouseEnter()
    {
        canvas.SetActive(true);
    }

    private void OnMouseExit()
    {
        canvas.SetActive(false);
    }

    public void UpdateText() 
    {
        itemName.text = gameObject.name;
    }
}
