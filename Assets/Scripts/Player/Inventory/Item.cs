using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    public int stackSize;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private ItemEnum itemCategory;
    [Range(0, 100)]
    public int dropChance;
    public ItemScriptableObject itemPrefab;
    
    public int StackSize { get => stackSize; private set => stackSize = value; }
    public Sprite Icon { get => icon; private set => icon = value; }
    public ItemEnum ItemCategory { get => itemCategory; private set => itemCategory = value; }
}
