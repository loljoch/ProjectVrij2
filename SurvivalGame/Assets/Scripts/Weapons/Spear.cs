using UnityEngine;

public class Spear : MonoBehaviour, IWeapon
{
    [Header("Sound settings")]
    [FMODUnity.EventRef]
    [SerializeField] private string attackSFX;

    [SerializeField] private int damage;
    [SerializeField] private Animations.WeaponAnimation weaponAnimation;
    private string weaponAnimationName;

    [SerializeField] private GameObject throwingSpear;
    public Animator PlayerAnim { get => playerAnim; set => playerAnim = value; }

    private Animator playerAnim;
    public float AttackInterval => attackInterval;
    public float attackInterval = 1f;

    private void Awake()
    {
        weaponAnimationName = Animations.GetWeaponAnimation(weaponAnimation);
    }

    public void DoAttackAnimation()
    {
        FMODUnity.RuntimeManager.PlayOneShot(attackSFX, transform.position);
        playerAnim.Play(weaponAnimationName);
    }

    public void Attack()
    {
        
    }
}
