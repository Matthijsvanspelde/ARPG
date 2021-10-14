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
    [Header("Offhand sprites")]
    [SerializeField]
    private SpriteRenderer offHand;
    [SerializeField]
    private Sprite defaultOffHand;
    private PlayerAttributes playerAttributes;


    [Header("Weapon")]
    [SerializeField]
    private Transform mainHand;
    [SerializeField]
    private GameObject unarmedPrefab;
    [SerializeField]
    private GameObject currentMainWeapon;

    private void Start()
    {
        playerAttributes = GetComponent<PlayerAttributes>();
    }

    public void SetDefaultSprite(GameObject item) 
    {
        EquimentSlotEnum equimentSlotEnum = item.GetComponent<EquipmentItem>().equipmentCategory;
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
                Destroy(currentMainWeapon);
                currentMainWeapon = Instantiate(unarmedPrefab, mainHand);
                break;
            case EquimentSlotEnum.OffHand:
                offHand.sprite = defaultOffHand;
                break;
            default:
                break;
        }
    }

    public void SetArmorSprite(GameObject item) 
    {
        EquipmentItem equipmentItem = item.GetComponent<EquipmentItem>();
        foreach (var spriteObject in equipmentItem.sprites)
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
                case SpriteEnum.Weapon:
                    Destroy(currentMainWeapon);
                    currentMainWeapon = Instantiate(item, mainHand);
                    currentMainWeapon.GetComponent<Item>().enabled = false;
                    currentMainWeapon.GetComponent<Collider>().enabled = false;
                    currentMainWeapon.GetComponent<SpriteRenderer>().sortingOrder = helm.sortingOrder;
                    break;
                case SpriteEnum.Offhand:
                    offHand.sprite = spriteResolver.sprite;
                    break;
                default:
                    break;
            }
        }
        
    }

    public void Equip(GameObject item) 
    {
        SetArmorSprite(item);
        playerAttributes.AddAttributes(item);
    }

    public void Unequip(GameObject item)
    {
        SetDefaultSprite(item);
        playerAttributes.RemoveAttributes(item);
    }

    public void Swap(GameObject newItem, GameObject oldItem)
    {
        SetArmorSprite(newItem);
        playerAttributes.AddAttributes(newItem);
        playerAttributes.RemoveAttributes(oldItem);
    }
}
