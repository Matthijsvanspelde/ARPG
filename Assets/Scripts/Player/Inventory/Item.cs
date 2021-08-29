using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private bool canBeStacked;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private ItemEnum itemCategory;
    [Range(0, 100)]
    public int dropChance;

    public bool CanBeStacked { get => canBeStacked; private set => canBeStacked = value; }
    public Sprite Icon { get => icon; private set => icon = value; }
    public ItemEnum ItemCategory { get => itemCategory; private set => itemCategory = value; }
}
