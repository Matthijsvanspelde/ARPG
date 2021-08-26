using UnityEngine;

public class SpriteSortOrder : MonoBehaviour
{
    private int baseSortingOrder; 
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseSortingOrder = spriteRenderer.sortingOrder;
    }

    private void LateUpdate()
    {
        spriteRenderer.sortingOrder = (int)(baseSortingOrder - transform.position.z);
    }
}
