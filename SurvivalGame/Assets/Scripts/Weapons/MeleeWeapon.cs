using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IWeapon
{
    [Header("Sound settings")]
    [FMODUnity.EventRef]
    [SerializeField] protected string attackSFX;

    [SerializeField] protected int damage;
    [SerializeField] private Animations.WeaponAnimation weaponAnimation;
    private string weaponAnimationName;

    [Header("Collider")]
    [SerializeField] private Vector3 center;
    [SerializeField] private Vector3 halfExtents;
    [SerializeField] private Transform hitDetection;

    public Animator PlayerAnim { get => playerAnim; set => playerAnim = value; }

    protected Animator playerAnim;
    public float AttackInterval => attackInterval;
    public float attackInterval = 1f;

    private void Awake()
    {
        weaponAnimationName = Animations.GetWeaponAnimation(weaponAnimation);
    }

    public virtual void DoAttackAnimation()
    {
        playerAnim.Play(weaponAnimationName);
    }

    protected bool CheckForHits(out Collider[] hits)
    {
        hits = Physics.OverlapBox(hitDetection.position + center, halfExtents, hitDetection.rotation, LayerMasks.Enemy);
        return (hits.Length > 0);
    }

    public virtual void Attack()
    {
        if (CheckForHits(out Collider[] hits))
        {
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].GetComponent<IDamagable>().TakeDamage(damage);
            }
            FMODUnity.RuntimeManager.PlayOneShot(attackSFX, transform.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(hitDetection.position + center, halfExtents);
    }
}

public interface IWeapon
{
    Animator PlayerAnim { get; set; }
    float AttackInterval { get; }
    void DoAttackAnimation();
    void Attack();
}
