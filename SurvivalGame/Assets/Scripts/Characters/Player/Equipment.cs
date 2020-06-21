using EasyAttributes;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public static System.Action<WeaponItem> EquipWeaponEvent;

    public WeaponItem weapon;
    [HideInInspector] public Transform current3dWeapon;

    [SerializeField] private Transform weaponHand;

    private void Awake()
    {
        EquipWeaponEvent += Equip;
    }

    private void OnDestroy()
    {
        EquipWeaponEvent -= Equip;
    }

    private void Start()
    {
        EquipWeaponEvent?.Invoke(weapon);
    }

    [Button]
    public void EquipCurrentHeld()
    {
        Equip(weapon);
    }

    public void Equip(WeaponItem weapon)
    {
        this.weapon = weapon;

        if(current3dWeapon != null)
        {
            Destroy(current3dWeapon.gameObject);
        }

        current3dWeapon = Instantiate(weapon.weaponPrefab, weaponHand).transform;
    }
}
