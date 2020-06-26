using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage;

    protected virtual void OnHit(IDamagable damagable)
    {
        if(damagable != null)
        {
            damagable.TakeDamage(damage);
        }
        Destroy(gameObject, 1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMasks.Player) return;
        OnHit(collision.gameObject.GetComponent<IDamagable>());
    }
}
