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
        }
    }
}
