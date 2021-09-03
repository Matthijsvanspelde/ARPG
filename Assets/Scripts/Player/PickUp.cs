using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    private void Update()
    {
        PickUpItem();
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

    private void PickUpItem()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Item"))
            {
                GameObject item = hit.transform.gameObject.GetComponent<Item>().itemPrefab.item;
                inventory.AddItem(item);
                Destroy(hit.transform.gameObject);
            }
        }
    }
}
