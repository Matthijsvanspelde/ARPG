using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField] 
    private RectTransform popupObject;
    [SerializeField] 
    private TextMeshProUGUI infoText;
    [SerializeField]
    private TextMeshProUGUI description;
    [SerializeField] 
    private Image Icon;
    [SerializeField] 
    private Vector3 offset;
    [SerializeField] 
    private float padding;
    

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        FollowCursor();
    }

    private void FollowCursor()
    {

        Vector3 newPos = Input.mousePosition + offset;
        newPos.z = 0f;
        float rightEdgeToScreenEdgeDistance = Screen.width - (newPos.x + popupObject.rect.width * canvas.scaleFactor / 2) - padding;
        if (rightEdgeToScreenEdgeDistance < 0)
        {
            newPos.x += rightEdgeToScreenEdgeDistance;
        }
        float leftEdgeToScreenEdgeDistance = 0 - (newPos.x - popupObject.rect.width * canvas.scaleFactor / 2) + padding;
        if (leftEdgeToScreenEdgeDistance > 0)
        {
            newPos.x += leftEdgeToScreenEdgeDistance;
        }
        float topEdgeToScreenEdgeDistance = Screen.height - (newPos.y + popupObject.rect.height * canvas.scaleFactor) - padding;
        if (topEdgeToScreenEdgeDistance < 0)
        {
            newPos.y += topEdgeToScreenEdgeDistance;
        }
        popupObject.transform.position = newPos;
    }

    public void DisplayInfo(ItemScriptableObject ScriptableObject)
    {
        Item item = ScriptableObject.item.GetComponent<Item>();
        infoText.text = item.name + " (" + item.rarity.name + ")";
        Color color = item.rarity.TextColour;
        infoText.color = color;       
        Icon.sprite = item.icon;
        description.text = item.description;
        gameObject.SetActive(true);
    }

    public void HideInfo()
    {
        gameObject.SetActive(false);
    }
}
