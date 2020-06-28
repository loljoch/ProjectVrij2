using UnityEngine;

public class Shield : MeleeWeapon
{
    public override void DoAttackAnimation()
    {
        PlayerCombat.OnChangeHpPlayerEvent += Block;
        base.DoAttackAnimation();
    }

    public override void Attack()
    {
        PlayerCombat.OnChangeHpPlayerEvent -= Block;
    }

    private void Block(int damage)
    {
        if(damage < 0)
        {
            PlayerCombat.HealingPlayerEvent?.Invoke(Mathf.Abs(damage));
            StunEnemy();
        }
    }

    private void StunEnemy()
    {
        if (CheckForHits(out Collider[] hits))
        {
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].GetComponent<IDamagable>().Stun(1.5f);
            }
            FMODUnity.RuntimeManager.PlayOneShot(attackSFX, transform.position);
        }
    }
}
