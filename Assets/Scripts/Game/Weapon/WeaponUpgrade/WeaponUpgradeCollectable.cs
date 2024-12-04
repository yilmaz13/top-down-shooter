using UnityEngine;

public class WeaponUpgradeCollectable : MonoBehaviour
{
    public WeaponUpgradeData WeaponUpgradeData;

    private void Initialize(WeaponUpgradeData upgradeData)
    {
        WeaponUpgradeData = upgradeData;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WeaponController weaponController))
        {
            weaponController.ApplyUpgrade(WeaponUpgradeData);
            //TODO:make poolable
            Destroy(gameObject);
        }
    }
}
