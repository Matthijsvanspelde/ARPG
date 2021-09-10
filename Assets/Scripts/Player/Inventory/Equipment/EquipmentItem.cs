using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : Item
{
    public EquimentSlotEnum itemCategory;
    public List<GameObject> spriteObjects = new List<GameObject>();

    [Header("Stat bonuses")]
    [SerializeField]
    private int strength;
    [SerializeField]
    private int health;
    [SerializeField]
    private int armor;

}
