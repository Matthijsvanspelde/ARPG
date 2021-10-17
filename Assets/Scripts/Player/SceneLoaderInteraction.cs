using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class SceneLoaderInteraction : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject target;
    private TargetRange targetRange;

    void Start()
    {
        targetRange = GetComponent<TargetRange>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        WalkToitem();
        GoToNewScene();
    }

    private void GoToNewScene() 
    {
        if (targetRange.IsAtTarget(target, 1.1f))
        {
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
}
