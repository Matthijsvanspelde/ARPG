using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void Awake()
    {
        target = GameObject.Find("Player").transform;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}
