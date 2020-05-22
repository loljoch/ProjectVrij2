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
		Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange, LayerMasks.Player);

		if(colliders[0] != null)
		{
			float distance = Vector3.Magnitude(colliders[0].transform.position - transform.position);
			if (distance <= attackRange)
			{
				EnemyAttackHitEvent?.Invoke(attackdamage);
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

		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, playerSpottedRange);

		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, movingToPlayerRange);
	}
}