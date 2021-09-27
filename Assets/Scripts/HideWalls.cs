using UnityEngine;

public class HideWalls : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Debug.Log("Hit");
            other.GetComponent<Renderer>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            other.GetComponent<Renderer>().enabled = true;
        }
    }
}
