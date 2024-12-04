using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Weapon[] _weapons;
    private int _currentWeaponIndex = 0;
    private Owner _owner; 
    void Start()
    {
        foreach (var weapon in _weapons)
        {
            weapon.firePoint = _firePoint;
        }
        SwitchWeapon(0);

        _owner= Owner.Player;

        SubscribeEvents();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _weapons[_currentWeaponIndex].TryShoot(_owner);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeapon(2);
        }
    }

    void SwitchWeapon(int index)
    {
        if (index >= 0 && index < _weapons.Length)
        {
            _currentWeaponIndex = index;
            for (int i = 0; i < _weapons.Length; i++)
            {
                _weapons[i].gameObject.SetActive(i == _currentWeaponIndex);
            }
        }
    }

    private void SubscribeEvents()
    {
        GameEvents.OnPlayerDead += ClearWeaponUpgrade;
    }

    private void UnsubscribeEvents()
    {
        GameEvents.OnPlayerDead -= ClearWeaponUpgrade;
    }

    private void ClearWeaponUpgrade()
    {
        foreach (var weapon in _weapons)
        {
            weapon.ResetUpgrades();
        }
    }
    public Weapon GetCurrentWeapon()
    {
        return _weapons[_currentWeaponIndex];
    }
    public void ApplyUpgrade(WeaponUpgradeData upgradeData)
    {
        GetCurrentWeapon().ApplyUpgrade(upgradeData);
    }

}
