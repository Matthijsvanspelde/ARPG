using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField]
    private float stoppingDistance = 0.8f;
    private GameObject target;
    private NavMeshAgent agent;
    private bool isInteracting = false;
    private TargetRange targetRange;

    private void Start()
    {
        targetRange = GetComponent<TargetRange>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Interact();
        SetTarget();
        WalkToTarget();
    }

    private void SetTarget()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("NPC") && !EventSystem.current.IsPointerOverGameObject())
            {
                target = hit.transform.gameObject;
                agent.stoppingDistance = stoppingDistance;
            }
            else if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Ground") && !EventSystem.current.IsPointerOverGameObject())
            {
                if (target != null)
                {
                    target.GetComponent<QuestGiver>().CloseQuestWindow();
                }               
                target = null;
                isInteracting = false;
            }
        }
    }

    private void WalkToTarget()
    {
        if (target != null)
        {
            agent.destination = target.transform.position;
        }
    }

    private void Interact() 
    {
        if (targetRange.IsAtTarget(target, stoppingDistance + 0.1f) && !isInteracting)
        {
            isInteracting = true;
            target.GetComponent<QuestGiver>().OpenQuestWindow(gameObject);
        }
    }

    public void ChooseOption() 
    { 
        
    }
}
