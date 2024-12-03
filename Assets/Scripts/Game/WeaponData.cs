using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[Serializable]
public class WeaponData 
{
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _armorPenetration;
    [SerializeField] private float _range;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _areaOfEffect;
    [SerializeField] private int _burstCount;
    [SerializeField] private float _burstDelay;

    public WeaponData(WeaponType weaponType, float speed, float damage, float armorPenetration, float range,
                      float fireRate, float areaOfEffect, int burstCount, float burstDelay)
    {
        _weaponType = weaponType;
        _speed = speed;
        _damage = damage;
        _armorPenetration = armorPenetration;
        _range = range;
        _fireRate = fireRate;
        _areaOfEffect = areaOfEffect;
        _burstCount = burstCount;
        _burstDelay = burstDelay;
        _speed = speed;
    }

    public WeaponType WeaponType => _weaponType;
    public float Speed => _speed;
    public float Damage => _damage;
    public float ArmorPenetration => _armorPenetration;
    public float Range => _range;
    public float FireRate => _fireRate;
    public float AreaOfEffect => _areaOfEffect;
    public int BurstCount => _burstCount;
    public float BurstDelay => _burstDelay;
}
