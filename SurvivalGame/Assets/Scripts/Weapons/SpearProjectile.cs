using UnityEngine;

public class SpearProjectile : Projectile
{
    [SerializeField] private ItemDrop itemDropSpear;

    protected override void OnHit(IDamagable damagable)
    {
        Instantiate(itemDropSpear, transform.position + Vector3.up, Quaternion.identity);
        base.OnHit(damagable);
    }
}
