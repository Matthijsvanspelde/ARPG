using UnityEngine;

public class TileCollider : MonoBehaviour
{
    public bool isColliding = false;

    private void OnTriggerEnter(Collider other)
    {
        isColliding = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isColliding = false;
    }
}
