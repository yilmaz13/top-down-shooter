using System;

[Serializable]
public class WeaponUpgradeData
{
    public WeaponUpgradeData(WeaponUpgradeType weaponUpgradeType, float value, bool isUpdated)
    {
        this.weaponUpgradeType = weaponUpgradeType;
        this.isUpdated = isUpdated;
        this.value = value;
    }

    public WeaponUpgradeType weaponUpgradeType;
    public bool isUpdated;
    public float value;
}