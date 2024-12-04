using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "ScriptableObjects/UpgradeData", order = 1)]
public class WeaponUpgradeDataSO: ScriptableObject
{
    public WeaponUpgradeType weaponUpgradeType;
    public float value;
}
