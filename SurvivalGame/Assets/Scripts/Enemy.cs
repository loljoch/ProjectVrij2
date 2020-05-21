﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[Header("Settings: ")]
	[SerializeField] private float maxHealth = 100f;
	[SerializeField] private float playerSpottedRange = 50f;
	[SerializeField] private float movingToPlayerRange = 40f;
	[SerializeField] private float movementSpeed = 4f;
	[SerializeField] private float attackDistance = 2f;
	[SerializeField] private float damage = 5f;
	[SerializeField] private float attackCooldownTimer = 3f;
	[SerializeField] private bool canAttack = true;
	[SerializeField] public float rotationDamping;

	[Header("References: ")]
	[SerializeField] private GameObject player;
	private Animator anim;

	private float playerDistance;
	private float rotationSpeed = 10f;
	private float tempMoveSpeed;
	private float currentHealth;

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
				if (playerDistance > attackDistance)
				{
					MoveTowardsPlayer();
				}
				else if (playerDistance <= attackDistance)
				{
					DoAttack();
				}
			}

			CheckDeathState();
		}
		else
		{
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
		anim.SetTrigger("Walking");
		transform.Translate(Vector3.forward * 10f * Time.deltaTime);
	}

	public virtual void TakeDamage(float damage)
	{
		currentHealth -= damage;
	}

	public virtual void ReactivateAttackState()
	{
		canAttack = true;
	}

	public virtual void DoAttack()
	{
		Debug.Log("Clamp: Attacking Player");
		anim.SetTrigger("Attack");
		tempMoveSpeed = 0f;
	}

	public virtual void CheckDeathState()
	{
		if (currentHealth <= 0)
		{
			anim.SetTrigger("Death");

			Destroy(gameObject, 5f);
		}
	}
}