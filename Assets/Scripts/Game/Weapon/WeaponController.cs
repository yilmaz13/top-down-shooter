using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Weapon[] weapons;
    private int currentWeaponIndex = 0;
    private Owner _owner; 
    void Start()
    {
        foreach (var weapon in weapons)
        {
            weapon.firePoint = _firePoint;
        }
        SwitchWeapon(0);

        _owner= Owner.Player;


    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            weapons[currentWeaponIndex].Shoot(_owner);
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
        if (index >= 0 && index < weapons.Length)
        {
            currentWeaponIndex = index;
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].gameObject.SetActive(i == currentWeaponIndex);
            }
        }
    }

    public Weapon GetCurrentWeapon()
    {
        return weapons[currentWeaponIndex];
    }
}
