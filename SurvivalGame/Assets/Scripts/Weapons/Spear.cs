using System.Collections;
using UnityEngine;

public class Spear : MonoBehaviour, IWeapon
{
    [Header("Sound settings")]
    [FMODUnity.EventRef]
    [SerializeField] private string attackSFX;

    [SerializeField] private int damage;
    [SerializeField] private Animations.WeaponAnimation weaponAnimation;
    private string weaponAnimationName;

    [SerializeField] private Rigidbody throwingSpear;
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
        var spear = Instantiate(throwingSpear, transform.position, playerAnim.transform.rotation);
        spear.velocity = playerAnim.transform.forward * projectileSpeed + (Vector3.up * projectileSpeed*0.1f);
    }
}
