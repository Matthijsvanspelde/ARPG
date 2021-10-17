using UnityEngine;

public class TargetRange : MonoBehaviour
{
    public bool IsAtTarget(GameObject target, float range)
    {
        if (target != null)
        {
            float dist = Vector3.Distance(target.transform.position, transform.position);

            if (dist <= range)
            {
                return true;
            }
        }
        return false;
    }
}
