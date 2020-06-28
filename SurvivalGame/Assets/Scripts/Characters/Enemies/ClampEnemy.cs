using Extensions;
using HighlightPlus;
using System.Collections;
using UnityEngine;

public class ClampEnemy : BaseEnemy, IInteractable
{
	[Header("Clam settings: ")]
	[Space()]
	[SerializeField] private float awakenRange;
	[SerializeField] private Vector3 startingPosition;
	[Space()]
	[SerializeField] private Transform meleeAttackPosition;
	[SerializeField] private float meleeAttackRange;
	[SerializeField] private int meleeAttackDamage;
	[Space()]
	[SerializeField] private Transform AOEAttackPosition;
	[SerializeField] private float AOEAttackRange;
	[SerializeField] private int AOEAttackDamage;

	private bool isAwake = false;
	protected override bool IsStunned { get => base.IsStunned; 
		set 
		{ 
			if(value == false)
			{
				Debug.Log("Stun is over");
			}
			base.IsStunned = value;
			StopAttacking();
		} 
	}

	#region CombatVariables
	private bool seesPlayer;
	private bool isDeciding = false;
	private bool canMelee;
	private bool canAOE;
	#endregion

	[Header("Interactable settings: ")]
	[SerializeField] private HighlightEffect highlightEffect;
	public string UseName => useName;
	[SerializeField] protected string useName;

	public float HoldTime => interactTime;
	[SerializeField] protected float interactTime = 2f;

	public string InteractionType => interactionType;

	[SerializeField] private string interactionType = "harvest the ";

	public bool HighLighted { set => highlightEffect.highlighted = value; }

	protected override void Awake()
	{
		base.Awake();
		anim.Play(Animations.Idle);
	}

	private void Update()
	{
		if (!isAwake) return;
		if (isAttacking || IsStunned || isDead) return;

		LookAtPlayer();

		if (!isDeciding)
		{
			isDeciding = true;
			StartCoroutine(MakeDeciding());
		}

		canMelee = (Vector3.Distance(player.transform.position, meleeAttackPosition.position) < meleeAttackRange);
		canAOE = (Vector3.Distance(player.transform.position, AOEAttackPosition.position) < AOEAttackRange);

		MoveTowardsPlayer();
	}

	private IEnumerator MakeDeciding()
	{
		yield return new WaitForSeconds(0.1f);
		if (canMelee)
		{
			anim.SetBool(Animations.Attack, true);
			isAttacking = true;
		} else if(canAOE)
		{
			anim.SetBool(Animations.Clamp.AOE, true);
			isAttacking = true;
		}

		isDeciding = false;
	}

	public void SetAwake()
	{
		if (isAwake) return;
		isAwake = true;
		SwitchLayer(true);
		AwakenBrothers();
	}

	private bool GetBrothren(out Collider[] hits)
	{
		hits = Physics.OverlapSphere(transform.position, awakenRange, LayerMasks.AllInteractable);
		return (hits.Length > 0);
	}

	private void AwakenBrothers()
	{
		if(GetBrothren(out Collider[] hits))
		{
			for (int i = 0; i < hits.Length; i++)
			{
				hits[i].GetComponent<ClampEnemy>()?.Interact();
			}
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

	private void SwitchLayer(bool enemy)
	{
		gameObject.layer = (enemy) ? LayerMasks.PhysicsEnemy : LayerMasks.PhysicsInteractable;
	}

	public void Interact()
	{
		anim.SetBool("WakeUp", true);
		gameObject.layer = LayerMask.NameToLayer("Default");
	}

	protected override void OnDrawGizmosSelected()
	{
		base.OnDrawGizmosSelected();

		GUIStyle style = new GUIStyle();
		style.normal.textColor = Color.red;
		style.fontSize = 18;

		Gizmos.color = Color.red;
		UnityEditor.Handles.Label(meleeAttackPosition.position + Vector3.up * meleeAttackRange, "Melee attack", style);
		Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackRange);

		UnityEditor.Handles.Label(AOEAttackPosition.position + Vector3.up * AOEAttackRange, "AOE attack", style);
		Gizmos.DrawWireSphere(AOEAttackPosition.position, AOEAttackRange);

		style.fontSize = 14;
		style.normal.textColor = Color.blue;
		Gizmos.color = Color.blue;

		if (GetBrothren(out Collider[] hits))
		{
			UnityEditor.Handles.Label(transform.position + Vector3.up, "Awaken lines", style);
			for (int i = 0; i < hits.Length; i++)
			{
				if (!hits[i].HasComponent<ClampEnemy>()) continue;
				Gizmos.DrawLine(transform.position + Vector3.up, hits[i].transform.position + Vector3.up);
			}
		}
	}

}