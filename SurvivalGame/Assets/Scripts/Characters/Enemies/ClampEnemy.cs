public class ClampEnemy : BaseEnemy
{
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

        if (distance < attackRange)
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
