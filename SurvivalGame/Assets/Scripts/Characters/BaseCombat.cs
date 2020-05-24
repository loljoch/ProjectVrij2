using UnityEngine;

public abstract class BaseCombat : MonoBehaviour, IDamagable
{
    [Header("Health Settings: ")]
    [SerializeField] protected int maxHealth = 3;

    [Header("Combat Settings: ")]
    [SerializeField] protected float attackRange = 5f;
    [SerializeField] protected float attackInterval = 1f;
    [SerializeField] protected int baseDamage = 1;
    [Tooltip("Only attack objects on this layer")]
    [SerializeField] protected LayerMask hitMask;

    protected virtual float AttackRange => attackRange;
    protected virtual int Damage => baseDamage;
    protected bool CanAttack => (Time.time > nextAttack); 
    protected int currentHealth = 0;

    protected float nextAttack = 0;

    protected virtual void Awake()
    {
        ResetHealth();
    }

    #region CombatFunctions
    protected virtual bool TryAttack()
    {
        if (!CanAttack) return false;
        nextAttack = Time.time + attackInterval;

        Attack();
        return true;
    }

    protected virtual bool CheckForHits()
    {
        return (Physics.OverlapSphere(transform.position, attackRange, hitMask).Length > 0);
    }

    protected virtual bool CheckForHits(out Collider[] hits)
    {
        hits = Physics.OverlapSphere(transform.position, attackRange, hitMask);
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

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.red;
        style.fontSize = 24;
        UnityEditor.Handles.Label(transform.position + Vector3.up * AttackRange, "Attack range", style);
    }
}

public interface IDamagable
{
    void TakeDamage(int _damageTaken);
}
