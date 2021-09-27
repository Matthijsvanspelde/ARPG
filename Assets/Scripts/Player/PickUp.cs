using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;
    private NavMeshAgent agent;
    private GameObject itemTarget;
    private GameObject itemToSave;
    private Item itemSelected;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        WalkToitem();
        PickUpItem();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gold") && !EventSystem.current.IsPointerOverGameObject())
        {
            inventory.inventoryData.gold += other.gameObject.GetComponent<Gold>().goldAmount;
            inventory.UpdateGoldText();
            Destroy(other.gameObject);
        }
    }

    private void WalkToitem()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            int mask = 1 << 10;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask) && hit.collider.CompareTag("Item") && !EventSystem.current.IsPointerOverGameObject())
            {
                agent.destination = hit.transform.position;
                agent.stoppingDistance = 0;
                itemToSave = hit.transform.gameObject.GetComponent<Item>().itemPrefab.item;
                itemTarget = hit.transform.gameObject;
            }
            else
            {
                itemTarget = null;
            }
        }
    }

    private void PickUpItem() 
    {
        if (itemTarget != null && IsAtTarget())
        {
            inventory.AddItem(itemToSave);
            Destroy(itemTarget);
        }
    }

    private bool IsAtTarget()
    {
        if (itemTarget != null)
        {
            float dist = Vector3.Distance(itemTarget.transform.position, transform.position);
            
            if (dist <= 0.5)
            {               
                return true;
            }
        }
        return false;
    }
}
