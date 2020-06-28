using UnityEngine;

public class Hammer : MeleeWeapon
{
    public override void Attack()
    {
        if (CheckForHits(out Collider[] hits))
        {
            for (int i = 0; i < hits.Length; i++)
            {
                var hit = hits[i].GetComponent<IDamagable>();
                hit.TakeDamage(damage);
                hit.Stun(1f);                
            }
            FMODUnity.RuntimeManager.PlayOneShot(attackSFX, transform.position);
        }
    }
}
