using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Clone")]
    public ItemScriptableObject itemPrefab;

    public Rarity rarity;
    public string description;
    public ItemEnum itemCategory;

    [Header("Properties")]
    public int stackSize;
    public Sprite icon;

    [Range(0, 100)]
    public int dropChance;

    [Header("Name Tag")]
    [SerializeField]
    private TMP_Text itemName;
    public GameObject canvas;

    private void Awake()
    {
        itemName.text = gameObject.name;
    }

    private void Start()
    {
        canvas.SetActive(false);
    }

    private void OnMouseEnter()
    {
        canvas.SetActive(true);
    }

    private void OnMouseExit()
    {
        canvas.SetActive(false);
    }

    public void UpdateText() 
    {
        itemName.text = gameObject.name;
    }
}
