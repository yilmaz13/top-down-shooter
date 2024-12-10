using UnityEngine;

public class AreaDamageProjectile : Projectile
{
    private float _areaOfEffect;

    public void Initialize(float speed, float range, float damage, float armorPenetration, Owner owner, float areaOfEffect)
    {
        base.Initialize(speed, range, damage, armorPenetration, owner);
        _areaOfEffect = areaOfEffect;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        ApplyAreaDamage();      
    }

    protected override void ReturnToPool()
    {
        base.ReturnToPool();
        PoolManager.Instance.ReturnObject(nameof(AreaDamageProjectile), this);
    }

    private void ApplyAreaDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _areaOfEffect);
        foreach (Collider hit in colliders)
        {
            if (hit.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.ApplyDamage(_damage, _armorPenetration);
                ReturnToPool();
            }
        }
    }
}
