using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    //TODO burst
    public override void Shoot(Owner owner)
    {
        int burstCount = weaponData.BurstCount;
       // curretProjectileCount = 0;        

       // if (curretProjectileCount < burstCount)
        {
            DOVirtual.DelayedCall(weaponData.BurstDelay, () =>
            {
                SpawnProjectile(weaponData.Speed, weaponData.Range, weaponData.Damage, weaponData.ArmorPenetration, owner);
            });
        }       
    }

    private void SpawnProjectile(float speed, float range, float damage, float armorPenetration, Owner owner)
    {
        Projectile projectile = ProjectilePool.Instance.GetObject() as Projectile;

        if (projectile != null)
        {
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = firePoint.rotation;
            projectile.Initialize(speed, range, damage, armorPenetration, owner);           
        }
    }
}
