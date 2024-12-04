using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponScriptableObjectData weaponDataSO;
    public WeaponData weaponData;
    public Transform firePoint;

    private List<WeaponUpgradeData> _activeUpgrades = new List<WeaponUpgradeData>();
    private float _lastShootTime;
    private float _cooldown;

    public abstract void Shoot(Owner owner);

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        weaponData.CopySOData(weaponDataSO);
        _cooldown = weaponData.FireRate;
    }

    public void TryShoot(Owner owner)
    {
        if (Time.time >= _lastShootTime + _cooldown)
        {
            Shoot(owner);
            _lastShootTime = Time.time;
        }
    }

    public void ApplyUpgrade(WeaponUpgradeData upgradeData)
    {
        if (!_activeUpgrades.Contains(upgradeData))
        {
            switch (upgradeData.weaponUpgradeType)
            {
                case WeaponUpgradeType.Scope:
                    weaponData.Range += upgradeData.value;
                    break;
                case WeaponUpgradeType.ArmorPiercingRounds:
                    weaponData.ArmorPenetration += upgradeData.value;
                    break;
                case WeaponUpgradeType.Barrel:
                    weaponData.Damage += upgradeData.value;
                    break;
            }
            _activeUpgrades.Add(upgradeData);
        }
    }

    public void ResetUpgrades()
    {
        _activeUpgrades.Clear();       
    }

    private void ApplyUpgrades()
    {
        foreach (var upgrade in weaponData.Upgrades)
        {
            ApplyUpgrade(upgrade);
        }
    }
}
