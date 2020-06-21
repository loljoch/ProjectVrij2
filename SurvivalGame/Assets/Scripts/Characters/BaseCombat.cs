using UnityEngine;

public abstract class BaseCombat : MonoBehaviour, IDamagable
{
    [Header("Health Settings: ")]
    [SerializeField] protected int maxHealth = 3;

    [Header("Combat Settings: ")]
    [SerializeField] protected float baseAttackRange = 5f;
    [SerializeField] protected float baseAttackInterval = 1f;
    [SerializeField] protected int baseDamage = 1;
    [Tooltip("Only attack objects on this layer")]
    [SerializeField] protected LayerMask hitMask;
    [SerializeField] protected Transform baseAttackFrom;

    protected virtual float AttackRange => baseAttackRange;
    protected virtual int Damage => baseDamage;
    protected virtual float AttackInterval => baseAttackInterval;
    protected virtual Transform AttackFrom => baseAttackFrom;
    protected bool CanAttack => (Time.time > nextAttack); 
    protected int currentHealth = 0;

    protected float nextAttack = 0;

    protected virtual void Awake()
    {
        ResetHealth();
        if(baseAttackFrom == null)
        {
            baseAttackFrom = transform;
        }
    }

    #region CombatFunctions
    protected virtual bool TryAttack()
    {
        if (!CanAttack) return false;
        nextAttack = Time.time + AttackInterval;

        Attack();
        return true;
    }

    protected virtual bool CheckForHits()
    {
        return (Physics.OverlapSphere(AttackFrom.position, baseAttackRange, hitMask).Length > 0);
    }

    protected virtual bool CheckForHits(out Collider[] hits)
    {
        hits = Physics.OverlapSphere(AttackFrom.position, baseAttackRange, hitMask);
        return (hits.Length > 0);
    }

    protected virtual void Attack()
    {
        if (CheckForHits(out Collider[] hits))
        {
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].GetComponent<IDamagable>().TakeDamage(Damage);
            }
        }
    }
    #endregion

    #region HealthFunctions
    public virtual void TakeDamage(int _damageTaken)
    {
        ChangeHealth(-_damageTaken);
        CheckDeathState();
    }
    
    protected void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    protected virtual void ChangeHealth(int _amount)
    {
        if (_amount == 0) return;

        currentHealth += _amount;
        Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    protected virtual void Heal(int _healAmount)
    {
        ChangeHealth(+_healAmount);
    }

    protected void CheckDeathState()
    {
        if(currentHealth <= 0)
        {
            OnDeath();
        }
    }

    protected abstract void OnDeath();
    #endregion

#if UNITY_EDITOR
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackFrom.position, AttackRange);

        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.red;
        style.fontSize = 24;
        UnityEditor.Handles.Label(transform.position + Vector3.up * AttackRange, "Attack range", style);
    }
#endif
}

public interface IDamagable
{
    void TakeDamage(int _damageTaken);
}
