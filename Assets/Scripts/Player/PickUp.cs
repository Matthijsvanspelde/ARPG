using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gold"))
        {
            inventory.gold += other.gameObject.GetComponent<Gold>().goldAmount;
            inventory.UpdateGoldText();
            Destroy(other.gameObject);
        }
    }
}
