using UnityEngine;

public abstract class Projectile : MonoBehaviour,
                                   IPoolable
{
    protected float _speed;
    protected float _range;
    protected float _damage;
    protected float _armorPenetration;
    protected Owner _owner;
    protected Vector3 _startPosition;
    protected bool _isInitialized;

    public void Initialize(float speed, float range, float damage, float armorPenetration, Owner owner)
    {
        _speed = speed;
        _range = range;
        _damage = damage;
        _armorPenetration = armorPenetration;
        _owner = owner;
        _startPosition = transform.position;
        _isInitialized = true;
    }

    protected virtual void Update()
    {
        if (!_isInitialized)
            return;

        if (Vector3.Distance(_startPosition, transform.position) > _range)
        {
            ReturnToPool();
        }
        else
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
    }

    protected abstract void OnTriggerEnter(Collider other);

    protected virtual void ReturnToPool()
    {
        _isInitialized = false;
    }

    public void OnSpawn() { }

    public void OnDespawn() { }

    public void SetActive(bool active)
    {
       gameObject.SetActive(active);
    }
}
