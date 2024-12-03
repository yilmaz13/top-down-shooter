
public class RocketLauncher : Weapon
{
    public override void Shoot(Owner owner)
    {
        SpawnProjectile(weaponData.Speed, weaponData.Range, weaponData.Damage, weaponData.ArmorPenetration, owner, weaponData.AreaOfEffect);
    }

    private void SpawnProjectile(float speed, float range, float damage, float armorPenetration, Owner owner, float areaOfEffect)
    {
        Projectile projectile = ProjectilePool.Instance.GetObject() as Projectile;

        if (projectile != null)
        {
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = firePoint.rotation;
            projectile.Initialize(speed, range, damage, armorPenetration, owner, areaOfEffect);
        }
    }
    
}
