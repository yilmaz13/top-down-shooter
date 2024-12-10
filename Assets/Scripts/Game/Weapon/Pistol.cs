
public class Pistol : Weapon
{
    public override void Shoot(Owner owner)
    {
        SpawnProjectile(weaponData.Speed, weaponData.Range, weaponData.Damage, weaponData.ArmorPenetration, owner);
    }
    private void SpawnProjectile(float speed, float range, float damage, float armorPenetration, Owner owner)
    {
        DirectDamageProjectile directProjectile = PoolManager.Instance.GetObject("DirectDamageProjectile") as DirectDamageProjectile;
        if (directProjectile != null)
        {
            directProjectile.transform.position = firePoint.position;
            directProjectile.transform.rotation = firePoint.rotation;
            directProjectile.Initialize(speed, range, damage, armorPenetration, owner);
        }
    }
}   
