
public class RocketLauncher : Weapon
{
    public override void Shoot(Owner owner)
    {
        SpawnProjectile(weaponData.Speed, weaponData.Range, weaponData.Damage, weaponData.ArmorPenetration, owner, weaponData.AreaOfEffect);
    }

    private void SpawnProjectile(float speed, float range, float damage, float armorPenetration, Owner owner, float areaOfEffect)
    {
        AreaDamageProjectile areaProjectile = PoolManager.Instance.GetObject("AreaDamageProjectile") as AreaDamageProjectile;
        if (areaProjectile != null)
        {
            areaProjectile.transform.position = firePoint.position;
            areaProjectile.transform.rotation = firePoint.rotation;
            areaProjectile.Initialize(speed, range, damage, armorPenetration, owner, areaOfEffect);
        }
    }
    
}
