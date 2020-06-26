﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour, IWeapon
{
    [Header("Sound settings")]
    [FMODUnity.EventRef]
    [SerializeField] private string attackSFX;

    [SerializeField] private int damage;
    [SerializeField] private Animations.WeaponAnimation weaponAnimation;
    private string weaponAnimationName;

    [SerializeField] private Rigidbody projectile;
    public Animator PlayerAnim { get => playerAnim; set => playerAnim = value; }

    private Animator playerAnim;
    public float AttackInterval => attackInterval;
    public float attackInterval = 1f;
    [SerializeField] private float projectileSpeed = 2;

    private void Awake()
    {
        weaponAnimationName = Animations.GetWeaponAnimation(weaponAnimation);
    }

    public void DoAttackAnimation()
    {
        playerAnim.Play(weaponAnimationName);
    }

    public void Attack()
    {
        FMODUnity.RuntimeManager.PlayOneShot(attackSFX, transform.position);
        var proj = Instantiate(projectile, transform.position, playerAnim.transform.rotation);
        proj.velocity = playerAnim.transform.forward * projectileSpeed;
    }
}
