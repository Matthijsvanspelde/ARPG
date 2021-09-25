using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : MonoBehaviour
{
    public EquimentSlotEnum equipmentCategory;
    public List<GameObject> sprites = new List<GameObject>();

    [Header("Base attribute bonuses")]
    public int strength;
    public int dexterity;
    public int health;
    public int armor;

}
