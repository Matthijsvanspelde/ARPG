using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class SceneLoaderInteraction : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject target;
    private bool isLoading = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        WalkToitem();
        GoToNewScene();
    }

    private void GoToNewScene() 
    {
        if (IsAtTarget() && !isLoading)
        {
            isLoading = true;
            target.GetComponent<SceneLoader>().LoadScene();
        }
    }

    private void WalkToitem()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("SceneLoader") && !EventSystem.current.IsPointerOverGameObject())
            {
                agent.destination = hit.transform.position;
                agent.stoppingDistance = 0;
                target = hit.transform.gameObject;
            }
            else
            {
                target = null;
            }
        }
    }

    private bool IsAtTarget()
    {
        if (target != null)
        {
            float dist = Vector3.Distance(target.transform.position, transform.position);
            if (dist <= 0.5)
            {
                return true;
            }
        }
        return false;
    }
}
