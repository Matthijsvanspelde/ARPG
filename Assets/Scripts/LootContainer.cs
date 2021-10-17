using UnityEngine;

public class LootContainer : MonoBehaviour
{
    private Loot lootTable;

    void Start()
    {
        lootTable = GetComponent<Loot>();
    }

    public void Open() 
    {
        lootTable.DropLoot();
        lootTable.DropGold();
        Destroy(gameObject);
    }
}
