public class PufferFishEnemy : BaseEnemy
{
	protected override void Awake()
	{
		hitMask = LayerMasks.Player;
		movement = GetComponent<BaseMovement>();
		anim = GetComponentInChildren<UnityEngine.Animator>();

		if (player == null)
		{
			player = FindObjectOfType<Player>().transform;
		}

		anim.SetTrigger(Animations.Idle);
	}

	private void Update()
	{
		if (isAttacking) return;

		float distance = DistanceToPlayer;

		if (distance > spotRange)
		{
			anim.SetTrigger(Animations.Idle);
			StopMoving();
			return;
		}

		if (distance < spotRange)
		{
			LookAtPlayer();
		}

		if (distance < baseAttackRange)
		{
			if (!TryAttack())
			{
				anim.SetTrigger(Animations.Idle);
			}
			return;
		}

		if (distance < movingToPlayerRange)
		{
			MoveTowardsPlayer();
		}
	}
}