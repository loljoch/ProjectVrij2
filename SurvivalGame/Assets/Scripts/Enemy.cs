﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public static Action<int> EnemyAttackHitEvent;

	[Header("Settings: ")]
	[SerializeField] private float maxHealth = 100f;
	[SerializeField] private float playerSpottedRange = 50f;
	[SerializeField] private float movingToPlayerRange = 40f;
	[SerializeField] private float movementSpeed = 4f;
	[SerializeField] public float rotationDamping = 3f;

	[Header("Combat Settings")]
	[SerializeField] protected float attackRange = 6f;
	[SerializeField] protected float attackInterval = 1f;
	[SerializeField] protected int attackdamage = 1;
	[SerializeField] protected float nextAttack = 0;

	[Header("References: ")]
	[SerializeField] private GameObject player = null;
	private Animator anim;

	private float playerDistance;
	private float rotationSpeed = 10f;
	private float currentHealth;
	private float tempSpeed;

	public virtual void Awake()
	{
		ResetHealth();
		anim = GetComponent<Animator>();
	}

	private void ResetHealth()
	{
		currentHealth = maxHealth;
	}

	public virtual void Update()
	{
		if (player != null)
		{
			playerDistance = Vector3.Distance(player.transform.position, transform.position);

			if(playerDistance > playerSpottedRange)
			{
				anim.SetTrigger("Idle");
			}

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

			CheckDeathState();
		}
		else
		{
			anim.SetTrigger("Idle");
			return;
		}
	}

	public virtual void LookAtPlayer()
	{
		Quaternion rotation = Quaternion.LookRotation(player.transform.position - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
	}

	public virtual void MoveTowardsPlayer()
	{
		tempSpeed = movementSpeed;
		anim.SetTrigger("Walking");
		transform.Translate(Vector3.forward * tempSpeed * Time.deltaTime);
	}

	public virtual void TakeDamage(int damage)
	{
		currentHealth -= damage;
	}

	public virtual void DoAttack()
	{
		tempSpeed = 0;
		anim.SetTrigger("Attack");
	}

	public virtual void CheckDeathState()
	{
		if (currentHealth <= 0)
		{
			tempSpeed = 0;
			anim.SetTrigger("Death");
			Destroy(gameObject, 5f);
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