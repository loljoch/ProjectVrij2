using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponItem", menuName = "Game/New Weapon", order = 6)]
public class WeaponItem : Item
{
    public int damage = 1;
    public float attackRange = 1f;
    public float attackInterval = 1f;
    public GameObject weaponPrefab;
}
