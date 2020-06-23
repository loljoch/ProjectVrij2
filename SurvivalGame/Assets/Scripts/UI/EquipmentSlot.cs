using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [Header("Components: ")]
    [SerializeField] private Image itemSprite;


    private void Awake()
    {
        PlayerCombat.EquipWeaponEvent += SetWeapon;
    }

    private void OnDestroy()
    {
        PlayerCombat.EquipWeaponEvent -= SetWeapon;
    }

    private void SetWeapon(WeaponItem weapon)
    {
        itemSprite.sprite = weapon.sprite;
        itemSprite.preserveAspect = true;
    }
}
