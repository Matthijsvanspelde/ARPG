using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    [Header("Weapon attribute bonuses")]
    public float attackSpeed = 2f;
    public float attackDamage = 4;
    public float attackRange = 0.8f;

    [Header("Sound effect")]
    public AudioClip attackSound;
}
