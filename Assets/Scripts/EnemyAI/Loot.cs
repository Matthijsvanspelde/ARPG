using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> lootTable = new List<GameObject>();
    [SerializeField]
    private int minGoldDrop = 0;
    [SerializeField]
    private int maxGoldDrop = 10;
    [SerializeField]
    private GameObject goldPrefab;

    public void DropLoot() 
    {
        foreach (var item in lootTable)
        {
            int randomChance = Random.Range(0, 100);
            if (randomChance < item.GetComponent<Item>().dropChance)
            {
                Debug.Log(item.name);
            }
        }       
    }

    public void DropGold() 
    {       
        int goldAmount = Random.Range(minGoldDrop, maxGoldDrop);
        GameObject goldObject = Instantiate(goldPrefab, transform);
        goldObject.GetComponent<Gold>().goldAmount = goldAmount;
    }
}
