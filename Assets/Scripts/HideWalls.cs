using UnityEngine;

public class HideWalls : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private Transform obstruction;

    void Update()
    {
        ViewObstructed();
    }

    private void ViewObstructed() 
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, target.position - transform.position, out hit, 100f))
        {
            hideWall(hit);
        }
    }

    private void hideWall(RaycastHit hit) 
    {
        if (hit.collider.gameObject.CompareTag("Wall"))
        {
            obstruction = hit.transform;
            obstruction.gameObject.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;


        }
        else if (obstruction != null)
        {
            obstruction.gameObject.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

        }
    }
}
