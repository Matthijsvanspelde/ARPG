using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField]
    private List<EquipmentSlot> equipmentSlots = new List<EquipmentSlot>();
    [Header("Helm sprites")]
    [SerializeField]
    private SpriteRenderer helm;
    [SerializeField]
    private Sprite defaultHelm;
    [Header("Chest sprites")]
    [SerializeField]
    private SpriteRenderer chest;
    [SerializeField]
    private Sprite defaultChest;
    [SerializeField]
    private SpriteRenderer leftArm;
    [SerializeField]
    private Sprite defaultLeftArm;
    [SerializeField]
    private SpriteRenderer rightArm;
    [SerializeField]
    private Sprite defaultRightArm;
    [Header("Pants sprites")]
    [SerializeField]
    private SpriteRenderer pelvis;
    [SerializeField]
    private Sprite defaultPelvis;
    [SerializeField]
    private SpriteRenderer leftLeg;
    [SerializeField]
    private Sprite defaultLeftLeg;
    [SerializeField]
    private SpriteRenderer rightLeg;
    [SerializeField]
    private Sprite defaultRightLeg;
    [Header("Feet sprites")]
    [SerializeField]
    private SpriteRenderer leftFoot;
    [SerializeField]
    private Sprite defaultLeftFoot;
    [SerializeField]
    private SpriteRenderer rightFoot;
    [SerializeField]
    private Sprite defaultRightFoot;
    [Header("Hand sprites")]
    [SerializeField]
    private SpriteRenderer leftHand;
    [SerializeField]
    private Sprite defaultLeftHand;
    [SerializeField]
    private SpriteRenderer rightHand;
    [SerializeField]
    private Sprite defaultRightHand;

    public void SetDefaultSprite(EquimentSlotEnum equimentSlotEnum) 
    {
        switch (equimentSlotEnum)
        {
            case EquimentSlotEnum.Helm:
                helm.sprite = defaultHelm;
                break;
            case EquimentSlotEnum.Chest:
                chest.sprite = defaultChest;
                leftArm.sprite = defaultLeftArm;
                rightArm.sprite = defaultRightArm;
                break;
            case EquimentSlotEnum.Hands:
                leftHand.sprite = defaultLeftHand;
                rightHand.sprite = defaultRightHand;
                break;
            case EquimentSlotEnum.Feet:
                leftFoot.sprite = defaultLeftFoot;
                rightFoot.sprite = defaultRightFoot;
                break;
            case EquimentSlotEnum.Ring:
                break;
            case EquimentSlotEnum.Neck:
                break;
            case EquimentSlotEnum.Pants:
                pelvis.sprite = defaultPelvis;
                leftLeg.sprite = defaultLeftLeg;
                rightLeg.sprite = defaultRightLeg;
                break;
            case EquimentSlotEnum.Weapon:
                break;
            case EquimentSlotEnum.Shield:
                break;
            default:
                break;
        }
    }

    public void SetArmorSprite(EquipmentItem item) 
    {
        Debug.Log("test");
        foreach (var spriteObject in item.spriteObjects)
        {
            SpriteResolver spriteResolver = spriteObject.GetComponent<SpriteResolver>();
            switch (spriteResolver.category)
            {
                case SpriteEnum.Helm:
                    helm.sprite = spriteResolver.sprite;
                    break;
                case SpriteEnum.Chest:
                    chest.sprite = spriteResolver.sprite;
                    break;
                case SpriteEnum.LeftArm:
                    leftArm.sprite = spriteResolver.sprite;
                    break;
                case SpriteEnum.RightArm:
                    rightArm.sprite = spriteResolver.sprite;
                    break;
                case SpriteEnum.LeftHand:
                    leftHand.sprite = spriteResolver.sprite;
                    break;
                case SpriteEnum.RightHand:
                    rightHand.sprite = spriteResolver.sprite;
                    break;
                case SpriteEnum.LeftFoot:
                    leftFoot.sprite = spriteResolver.sprite;
                    break;
                case SpriteEnum.RightFoot:
                    rightFoot.sprite = spriteResolver.sprite;
                    break;
                case SpriteEnum.Pelvis:
                    pelvis.sprite = spriteResolver.sprite;
                    break;
                case SpriteEnum.LeftLeg:
                    leftLeg.sprite = spriteResolver.sprite;
                    break;
                case SpriteEnum.RightLeg:
                    rightLeg.sprite = spriteResolver.sprite;
                    break;
                default:
                    break;
            }
        }
        
    }
}
