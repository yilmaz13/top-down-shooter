using System;
using System.Collections.Generic;

[Serializable]
public class WeaponData
{
    public WeaponType WeaponType;
    public float Speed;
    public float Damage;
    public float ArmorPenetration;
    public float Range;
    public float FireRate;
    public float AreaOfEffect;
    public int BurstCount;
    public float BurstDelay;
    public List<WeaponUpgradeData> Upgrades;

    public WeaponData(WeaponType weaponType, float speed, float damage, float armorPenetration, float range,
                      float fireRate, float areaOfEffect, int burstCount, float burstDelay,
                      List<WeaponUpgradeData> _upgrades)
    {
        WeaponType = weaponType;
        Speed = speed;
        Damage = damage;
        ArmorPenetration = armorPenetration;
        Range = range;
        FireRate = fireRate; 
        AreaOfEffect = areaOfEffect; 
        BurstCount = burstCount;
        BurstDelay = burstDelay; 
        Speed = speed;
        //value referesn
        Upgrades = _upgrades;
    }  

    public void CopySOData(WeaponScriptableObjectData weaponDataSO)
    {
        WeaponType = weaponDataSO.WeaponType;
        Speed = weaponDataSO.Speed;
        Damage = weaponDataSO.Damage;
        ArmorPenetration = weaponDataSO.ArmorPenetration;
        Range = weaponDataSO.Range;
        FireRate = weaponDataSO.FireRate;
        AreaOfEffect = weaponDataSO.AreaOfEffect;
        BurstCount = weaponDataSO.BurstCount;
        BurstDelay = weaponDataSO.BurstDelay;

        List<WeaponUpgradeData> weaponUpgrades = new List<WeaponUpgradeData>();

        foreach (var upgrade in weaponDataSO.Upgrades)
        {
            weaponUpgrades.Add(new WeaponUpgradeData(upgrade.weaponUpgradeType, upgrade.value));
        }

        Upgrades = weaponUpgrades;
    }
}
