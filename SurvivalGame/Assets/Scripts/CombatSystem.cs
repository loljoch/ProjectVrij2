using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombatSystem : MonoBehaviour
{
	public static Action<int> MeleeAttackHitEvent;

	[Header("Settings: ")]
	[SerializeField] private float attackRange = 5f;
	[SerializeField] private float attackInterval = 1f;
	[SerializeField] private int meleeDamage = 110;

	private float nextAttack = 0;

	private void Update()
	{
		if(Input.GetButtonDown("Fire1"))
		{
			MeleeAttack();
		}
	}

	private void MeleeAttack()
	{
		if(Time.time > nextAttack)
		{
			nextAttack = Time.time + attackInterval;

			Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);

			if(colliders[0] != null)
			{
				float distance = Vector3.Magnitude(colliders[0].transform.position - transform.position);
				if (distance <= attackRange)
				{
					MeleeAttackHitEvent?.Invoke(meleeDamage);
				}
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackRange);
	}
}