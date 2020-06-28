public class ClampEnemy : BaseEnemyBehaviour
{

	private bool ClampNotDetected = true;

	protected override void Awake()
	{
		base.Awake();
		ClampNotDetected = true;
		anim.SetTrigger("IdleAsleep");
	}

	protected override void Attack()
	{
		base.Attack();
		anim.SetTrigger("MeleeAttack");
	}

	private void Update()
	{
		if (isAttacking) return;

		float _distance = DistanceToPlayer;

		if(ClampNotDetected)
		{
			if (_distance > spotRange)
			{
				anim.SetTrigger("IdleAsleep");
				StopMoving();
				return;
			}

			if (_distance < movingToPlayerRange)
			{
				anim.SetTrigger("WakingUp");
				return;
			}
		}
		else if(!ClampNotDetected)
		{
			if (_distance > spotRange)
			{
				anim.SetTrigger(Animations.Idle);
				StopMoving();
				return;
			}

			if (_distance < spotRange)
			{
				LookAtPlayer();
			}

			if (_distance < movingToPlayerRange)
			{
				MoveTowardsPlayer();
			}

			if (_distance < baseAttackRange)
			{
				if (!TryAttack())
				{
					anim.SetTrigger("Idle");
				}
				return;
			}	
		}
	}

	public void WakingToIdle()
	{
		ClampNotDetected = false;
		anim.Play("Awake (Idle)");
	}

/*
    private void Update()
    {
        if (isAttacking) return;

        float distance = DistanceToPlayer;

        if(distance > spotRange)
        {
            anim.SetTrigger(Animations.Idle);
            StopMoving();
            return;
        }

        if(distance < spotRange)
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
*/
}