using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : Item
{
    public EquimentSlotEnum equipmentCategory;
    public List<GameObject> spriteObjects = new List<GameObject>();

    [Header("Stat bonuses")]
    public int strength;
    public int health;
    public int armor;

}
