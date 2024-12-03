using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponScriptableObjectData weaponDataSO;
    public WeaponData weaponData;
    public Transform firePoint;

    public abstract void Shoot(Owner owner);

    private void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        weaponData = new WeaponData(WeaponType.Rifle, weaponDataSO.Speed, weaponDataSO.Damage, weaponDataSO.ArmorPenetration, weaponDataSO.Range, 
                                    weaponDataSO.FireRate, weaponDataSO.AreaOfEffect, weaponDataSO.BurstCount, weaponDataSO.BurstDelay);

    }
}