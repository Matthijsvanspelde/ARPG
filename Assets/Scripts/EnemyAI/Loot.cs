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
                var position = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
                GameObject newItem = Instantiate(item, transform.position + position, item.transform.rotation);
                newItem.name = item.name;
                newItem.GetComponent<Item>().UpdateText();
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
