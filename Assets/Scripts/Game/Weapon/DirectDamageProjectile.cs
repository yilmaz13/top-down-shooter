using UnityEngine;

public class DirectDamageProjectile : Projectile
{
    protected override void OnTriggerEnter(Collider other)
    {
        ApplyDirectDamage(other);      
    }

    private void ApplyDirectDamage(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.ApplyDamage(_damage, _armorPenetration);
            ReturnToPool();
        }
    }

    protected override void ReturnToPool()
    {
        PoolManager.Instance.ReturnObject(nameof(DirectDamageProjectile), this);
    }
}
