using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Projectile : MonoBehaviour,
                          IPoolable
{
    private float _speed;
    private float _range;
    private float _damage;
    private float _armorPenetration;
    private float _areaOfEffect;
    private Owner _owner;
    private Vector3 _startPosition;
    public void Initialize(float speed, float range, float damage, float armorPenetration, Owner owner, float areaOfEffect = 0)
    {
        _speed = speed;
        _range = range;
        _damage = damage;
        _armorPenetration = armorPenetration;
        _areaOfEffect = areaOfEffect;
        _owner = owner;
        _startPosition = transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(_startPosition, transform.position) > _range)
        {
            ProjectilePool.Instance.ReturnProjectile(this);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
    }  

    private void OnTriggerEnter(Collider other)
    {
        if (_areaOfEffect > 0)
        {
            ApplyAreaDamage();
        }
        else
        {
            ApplyDirectDamage(other);
        }

        ReturnToPool();
    }

    private void ApplyAreaDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _areaOfEffect);
        foreach (Collider hit in colliders)
        {
            if (hit.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.ApplyDamage(_damage, _armorPenetration);
            }
        }
    }

    private void ApplyDirectDamage(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.ApplyDamage(_damage, _armorPenetration);
        }
    }
    private void ReturnToPool()
    {
        ProjectilePool.Instance.ReturnProjectile(this);
    }

    public void OnSpawn()
    {

    }

    public void OnDespawn()
    {

    }
}
