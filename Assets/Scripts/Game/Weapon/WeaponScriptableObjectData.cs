using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponSO", menuName = "ScriptableObjects/WeaponSO", order = 0)]
public class WeaponScriptableObjectData : ScriptableObject
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
    [SerializeField] private List<WeaponUpgradeDataSO> _upgrades;

    public WeaponType WeaponType => _weaponType;
    public float Speed => _speed;
    public float Damage => _damage;
    public float ArmorPenetration => _armorPenetration;
    public float Range => _range;
    public float FireRate => _fireRate;
    public float AreaOfEffect => _areaOfEffect;
    public int BurstCount => _burstCount;
    public float BurstDelay => _burstDelay;
    public List<WeaponUpgradeDataSO> Upgrades => _upgrades;
}
