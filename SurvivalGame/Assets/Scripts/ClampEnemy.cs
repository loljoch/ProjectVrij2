using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ClampEnemy : Enemy
{
	public override void DoAttack()
	{
		base.DoAttack();
	}

	private void ClampAttack()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);

		foreach (Collider hit in colliders)
		{
			if (hit && hit.tag == Tags.Player)
			{
				float distance = Vector3.Magnitude(hit.transform.position - transform.position);
				if (distance <= attackRange)
				{
					if (EnemyAttackHitEvent != null)
					{
						EnemyAttackHitEvent(attackdamage);
					}
				}
			}
		}
	}

	public override void CheckDeathState()
	{
		base.CheckDeathState();
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackRange);
	}
}