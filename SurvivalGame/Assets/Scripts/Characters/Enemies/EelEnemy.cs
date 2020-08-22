using Extensions;
using System.Collections;
using UnityEngine;

public class EelEnemy : BaseEnemy
{
	protected override bool IsStunned
	{
		get => base.IsStunned;
		set
		{
			base.IsStunned = value;
			StopAttacking();
		}
	}

	#region CombatVariables
	private bool seesPlayer;
	private bool canMelee;
	private bool canAOE;
	#endregion

	[Header("Eel settings: ")]
	[SerializeField] private Animator eelAnim;
	[Space()]
	[SerializeField] private Transform meleeAttackPosition;
	[SerializeField] private float meleeAttackRange;
	[SerializeField] private int meleeAttackDamage;
	[Space()]
	[SerializeField] private Transform AOEAttackPosition;
	[SerializeField] private float AOEAttackRange;
	[SerializeField] private int AOEAttackDamage;

	[Header("Sounds settings: ")]
	[FMODUnity.EventRef]
	[SerializeField] private string tailwhipSFX;
	[FMODUnity.EventRef]
	[SerializeField] private string aoeattackSFX;
	[FMODUnity.EventRef]
	[SerializeField] private string deathSFX;


	protected override void Awake()
	{
		base.Awake();
		anim = eelAnim;
	}

	private void Update()
	{
		if (DistanceToPlayer > spotRange) return;
		if (isAttacking || IsStunned || isDead) return;

		LookAtPlayer();

		canMelee = (Vector3.Distance(player.transform.position, meleeAttackPosition.position) < meleeAttackRange);
		canAOE = (Vector3.Distance(player.transform.position, AOEAttackPosition.position) < AOEAttackRange);
		if(canAOE || canMelee)
		{
			MakeDecision();
		}

		MoveTowardsPlayer();
	}

	private void MakeDecision()
	{
		int rAttack = Random.Range(0, 2);
		if (rAttack > 0)
		{
			FMODUnity.RuntimeManager.PlayOneShot(tailwhipSFX, transform.position);
			anim.SetBool(Animations.Attack, true);
			isAttacking = true;
		} else
		{
			FMODUnity.RuntimeManager.PlayOneShot(aoeattackSFX, transform.position);
			anim.SetBool(Animations.Clamp.AOE, true);
			isAttacking = true;
		}
	}


	public void MeleeAttack()
	{
		baseAttackFrom = meleeAttackPosition; // Position for the melee attack
		baseAttackRange = meleeAttackRange;
		baseDamage = meleeAttackDamage;
		Attack();
	}

	public void AOEAttack()
	{
		baseAttackFrom = AOEAttackPosition; // Position for the AOE attack
		baseAttackRange = AOEAttackRange;
		baseDamage = AOEAttackDamage;
		Attack();
	}

	public void StopAttacking()
	{
		anim.SetBool(Animations.Clamp.AOE, false);
		anim.SetBool(Animations.Attack, false);
		isAttacking = false;
	}

	protected override void OnDeath()
	{
		isDead = true;
		anim.SetTrigger(Animations.Death);
	}

	public override void Die()
	{
		FMODUnity.RuntimeManager.PlayOneShot(deathSFX, transform.position);
		base.Die();
	}


	//protected override void OnDrawGizmosSelected()
	//{
	//	base.OnDrawGizmosSelected();

	//	GUIStyle style = new GUIStyle();
	//	style.normal.textColor = Color.red;
	//	style.fontSize = 18;

	//	Gizmos.color = Color.red;
	//	UnityEditor.Handles.Label(meleeAttackPosition.position + Vector3.up * meleeAttackRange, "Melee attack", style);
	//	Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackRange);

	//	UnityEditor.Handles.Label(AOEAttackPosition.position + Vector3.up * AOEAttackRange, "AOE attack", style);
	//	Gizmos.DrawWireSphere(AOEAttackPosition.position, AOEAttackRange);
	//}

}