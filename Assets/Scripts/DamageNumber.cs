using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    [SerializeField]
    private GameObject numberPrefab;

    public void Create(int damage) 
    {
        GameObject damageNumber = Instantiate(numberPrefab, transform);
        DamageNumberUI damageNumberUI = damageNumber.GetComponent<DamageNumberUI>();
        damageNumberUI.SetValue(damage);
    }
}
