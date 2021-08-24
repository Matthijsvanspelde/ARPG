using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldOfView : MonoBehaviour
{
	public float viewRadius;
	public LayerMask targetMask;
	public LayerMask obstacleMask;
	[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();

    private void Update()
    {
		FoundPlayer();
	}

    public bool FoundPlayer()
	{
		visibleTargets.Clear();
		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++)
		{
			Transform target = targetsInViewRadius[i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			float dstToTarget = Vector3.Distance(transform.position, target.position);
			if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
			{
				visibleTargets.Add(target);
				return true;
			}			
		}
		return false;
	}
}
