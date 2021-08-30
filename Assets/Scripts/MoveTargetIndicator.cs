using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTargetIndicator : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnEnable()
    {
        
    }

    void Update()
    {
        SetIndicatorPosition();
    }

    private void SetIndicatorPosition() 
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(1))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Ground"))
            {
                Vector3 targetPosition = hit.point;
                gameObject.transform.position = targetPosition;
            }
        }
    }
}
