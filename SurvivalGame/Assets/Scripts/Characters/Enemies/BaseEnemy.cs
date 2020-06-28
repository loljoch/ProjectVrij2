using Extensions.Vector;
using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseMovement))]
[RequireComponent(typeof(Animator))]
public abstract class BaseEnemy : BaseCombat
{
    [Header("Ënemy settings: ")]
    [SerializeField] protected float spotRange = 50f;
    [SerializeField] protected float movingToPlayerRange = 40f;

    [Header("References: ")]
    [SerializeField] protected Transform player = null;
    [SerializeField] private List<Renderer> rend = null;

    protected BaseMovement movement;
    protected Animator anim;

    protected bool isDead = false;

    protected float DistanceToPlayer => Vector3.Distance(transform.position, player.position);
    protected Vector3 DirectionToPlayer => transform.position.GetDirectionTo(player.position);
    protected bool isAttacking = false;
    protected override bool IsStunned 
    { 
        get => base.IsStunned; 
        set
        {
            base.IsStunned = value;
            anim.SetBool("Stunned", value);
            isAttacking = false;
        }
    }

    [SerializeField] private LootDrop loot;

    protected override void Awake()
    {
        base.Awake();
        hitMask = LayerMasks.Player;
        movement = GetComponent<BaseMovement>();
        anim = GetComponent<Animator>();

        if (player == null)
        {
            player = FindObjectOfType<Player>().transform;
        }

        anim.SetTrigger(Animations.Idle);
    }

    public override void Stun(float time)
    {
        IsStunned = true;
        StartCoroutine(StunWaitTime(time));
    }

    private IEnumerator StunWaitTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        IsStunned = false;
    }

    protected void LookAtPlayer()
    {
        movement.LerpLookDirection(DirectionToPlayer);
    }

    protected void MoveTowardsPlayer()
    {
        anim.SetTrigger(Animations.Walking);
        movement.Move(DirectionToPlayer);
    }

    protected void StopMoving()
    {
        movement.Move(Vector3.zero);
    }

    protected override bool TryAttack()
    {
        StopMoving();
        return base.TryAttack();
    }

    public void AnimAttack()
    {
        base.Attack();
        isAttacking = false;
    }

    protected override void OnDeath()
    {
        isAttacking = false;
        anim.SetBool(Animations.Death, true);
        isDead = true;
        StopMoving();
    }

    public override void TakeDamage(int _damageTaken)
    {
        base.TakeDamage(_damageTaken);
        StartCoroutine(HitEffect());
    }

    private IEnumerator HitEffect()
    {
        for (int i = 0; i < rend.Count; i++)
        {
            rend[i].material.SetColor("_Color", Color.red);
        }
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < rend.Count; i++)
        {
            rend[i].material.SetColor("_Color", Color.white);
        }
    }

    public virtual void Die()
    {
        loot.Spawn(transform.position + Vector3.up);
        Destroy(gameObject);
    }

#if UNITY_EDITOR
    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, spotRange);

        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.blue;
        style.fontSize = 24;
        UnityEditor.Handles.Label(transform.position + Vector3.up * spotRange, "Spot range", style);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, movingToPlayerRange);

        style.normal.textColor = Color.yellow;
        UnityEditor.Handles.Label(transform.position + Vector3.up * movingToPlayerRange, "Move range", style);
    }
#endif
}