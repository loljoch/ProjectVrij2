using Extensions.Vector;
using UnityEngine;

[RequireComponent(typeof(BaseMovement))]
[RequireComponent(typeof(Animator))]
public class BaseEnemyBehaviour : BaseCombat
{
	[Header("Ënemy settings: ")]
	[SerializeField] protected float spotRange = 50f;
	[SerializeField] protected float movingToPlayerRange = 40f;

	[Header("References: ")]
	[SerializeField] protected Transform player = null;

	protected BaseMovement movement;
	protected Animator anim;

	protected float DistanceToPlayer => Vector3.Distance(transform.position, player.position);
	protected Vector3 DirectionToPlayer => transform.position.GetDirectionTo(player.position);
	protected bool isAttacking = false;

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
	}

	protected void LookAtPlayer()
	{
		movement.LerpLookDirection(DirectionToPlayer);
	}

	protected virtual void MoveTowardsPlayer()
	{
		anim.SetTrigger(Animations.Walking);
		movement.Move(DirectionToPlayer);
	}

	protected override void Attack()
	{
		isAttacking = true;
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

	protected void StopMoving()
	{
		movement.Move(Vector3.zero);
	}

	protected override void OnDeath()
	{
		isAttacking = true;
		anim.SetTrigger(Animations.Death);
		StopMoving();
		Destroy(gameObject, 3f);
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
		//UnityEditor.Handles.Label(transform.position + Vector3.up * spotRange, "Spot range", style);

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, movingToPlayerRange);

		style.normal.textColor = Color.yellow;
		// UnityEditor.Handles.Label(transform.position + Vector3.up * movingToPlayerRange, "Move range", style);
	}
#endif
}