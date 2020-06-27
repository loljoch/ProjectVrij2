public class Spear : RangedWeapon 
{
    public override void Attack()
    {
        base.Attack();

        if (!UIManager.Instance.inventory.Contains(ammo.id))
        {
            ItemOptionMenu.Instance.UnEquip();
        }
    }
}
