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

			foreach (Collider hit in colliders)
			{
				if(hit && hit.tag == Tags.EnemyClamp)
				{
					float distance = Vector3.Magnitude(hit.transform.position - transform.position);
					if(distance <= attackRange)
					{
						Debug.Log("PlayerAttack");

						if(MeleeAttackHitEvent != null)
						{
							MeleeAttackHitEvent(meleeDamage);
						}
					}
				}
			}
		}
	}
}