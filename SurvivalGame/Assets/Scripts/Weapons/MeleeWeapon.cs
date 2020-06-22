using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private int damage;
    [SerializeField] private string animationName;

    [Header("Collider")]
    [SerializeField] private Vector3 center;
    [SerializeField] private Vector3 halfExtents;

    public Animator PlayerAnim { get; set; }

    private Animator playerAnim;
    public float AttackInterval => attackInterval;
    public float attackInterval = 1f;

    public void DoAttackAnimation()
    {
        playerAnim.Play(animationName);
    }

    private bool CheckForHits(out Collider[] hits)
    {
        hits = Physics.OverlapBox(transform.position + center, halfExtents);
        return (hits.Length > 0);
    }

    public void Attack()
    {
        if (CheckForHits(out Collider[] hits))
        {
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].GetComponent<IDamagable>().TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + center, halfExtents);
    }
}

public interface IWeapon
{
    Animator PlayerAnim { get; set; }
    float AttackInterval { get; }
    void DoAttackAnimation();
    void Attack();
}
