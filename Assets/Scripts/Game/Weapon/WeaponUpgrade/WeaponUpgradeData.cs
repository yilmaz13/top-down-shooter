using System;

[Serializable]
public class WeaponUpgradeData
{
    public WeaponUpgradeData(WeaponUpgradeType weaponUpgradeType, float value)
    {
        this.weaponUpgradeType = weaponUpgradeType;
        this.value = value;
    }

    public WeaponUpgradeType weaponUpgradeType;
    public float value;
}