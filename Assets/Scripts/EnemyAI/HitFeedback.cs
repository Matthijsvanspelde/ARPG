using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFeedback : MonoBehaviour
{
    
    private Color originalColor;
    [SerializeField]
    private Color flashColor;
    [SerializeField]
    private float flashTime;
    [SerializeField]
    private List<SpriteRenderer> renderers;

    private void Awake()
    {
        originalColor = Color.white;
    }

    public void Flash()
    {
        foreach (var renderer in renderers)
        {
            renderer.color = flashColor;
        }
        Invoke("ResetColor", flashTime);
    }

    private void ResetColor()
    {
        foreach (var renderer in renderers)
        {
            renderer.color = originalColor;
        }
    }
}
