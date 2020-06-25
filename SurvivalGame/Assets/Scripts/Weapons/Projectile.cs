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
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnHit(collision.gameObject.GetComponent<IDamagable>());
    }
}
