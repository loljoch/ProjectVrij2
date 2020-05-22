using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public static Action<int> EnemyAttackHitEvent;

	[Header("Settings: ")]
	[SerializeField] protected float maxHealth = 100f;
	[SerializeField] protected float playerSpottedRange = 50f;
	[SerializeField] protected float movingToPlayerRange = 40f;
	[SerializeField] protected float movementSpeed = 4f;
	[SerializeField] protected float rotationDamping = 3f;

	[Header("Combat Settings")]
	[SerializeField] protected float attackRange = 6f;
	[SerializeField] protected float attackInterval = 1f;
	[SerializeField] protected int attackdamage = 1;
	[SerializeField] protected float nextAttack = 0;

	[Header("References: ")]
	[SerializeField] private GameObject player = null;
	private Animator anim;

	private float playerDistance;
	private float currentHealth;

	public virtual void Awake()
	{
		ResetHealth();
		anim = GetComponent<Animator>();
		anim.SetTrigger("Idle");
	}

	public virtual void Update()
	{
		if (player == null)
		{
			anim.SetTrigger("Idle");
		}

		if (player != null)
		{
			playerDistance = Vector3.Distance(player.transform.position, transform.position);

			if (playerDistance < playerSpottedRange)
			{
				LookAtPlayer();
			}

			if (playerDistance < movingToPlayerRange)
			{
				if (playerDistance > attackRange)
				{
					MoveTowardsPlayer();
				}
				else if (playerDistance <= attackRange)
				{
					DoAttack();
				}
			}

		}
		else
		{
			return;
		}
	}

	private void ResetHealth()
	{
		currentHealth = maxHealth;
	}

	public virtual void LookAtPlayer()
	{
		Quaternion _rotation = Quaternion.LookRotation(player.transform.position - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, Time.deltaTime * rotationDamping);
	}

	public virtual void MoveTowardsPlayer()
	{
		anim.SetTrigger("Walking");
		transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
	}

	public virtual void TakeDamage(int damage)
	{
		currentHealth -= damage;
		CheckDeathState();
	}

	public virtual void DoAttack()
	{
		anim.SetTrigger("Attack");
	}

	public virtual void CheckDeathState()
	{
		if (currentHealth <= 0)
		{
			movementSpeed = 0;
			anim.SetTrigger("Death");
			Destroy(gameObject, 3f);
		}
	}

	private void OnEnable()
	{
		CombatSystem.MeleeAttackHitEvent += TakeDamage;
	}

	private void OnDisable()
	{
		CombatSystem.MeleeAttackHitEvent -= TakeDamage;
	}
}