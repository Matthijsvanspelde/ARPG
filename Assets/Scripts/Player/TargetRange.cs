using UnityEngine;

public class TargetRange : MonoBehaviour
{
    public bool IsAtTarget(GameObject target, float range)
    {
        if (target != null)
        {
            float offset = target.GetComponent<Collider>().bounds.size.x;
            float dist = Vector3.Distance(target.transform.position, transform.position);

            if (dist <= range + offset)
            {
                return true;
            }
        }
        return false;
    }
}
