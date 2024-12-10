using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameResources", menuName = "ScriptableObjects/GameResources", order = 0)]

public class GameResources : ScriptableObject
{
    [Header("Config")]
    [SerializeField] private float _followCameraSmootTime;
    [SerializeField] private float _playerBaseSpeed;
    [SerializeField] private float _playerBaseHealth;
    [SerializeField] private float _playerBaseArmor;

    //birden çok enemy olursa farklý bir resource oluþturulabilir
    [SerializeField] private float _enemyBaseSpeed;
    [SerializeField] private float _enemyBaseHealth;
    [SerializeField] private float _enemyBaseArmor;

    [Header("Prefabs")]
    [SerializeField] private List<GameObject> _weaponList;
    [SerializeField] private GameObject _playerPrefabs;
    [SerializeField] private GameObject _enemyPrefabs;
    [SerializeField] private GameObject _healthSliderPrefabs;
    [SerializeField] private GameObject _armorSliderPrefabs;
    [SerializeField] private List<GameObject> _weaponUpgradePrefabs;

    public float FollowCameraSmootTime => _followCameraSmootTime;
    public float PlayerBaseSpeed => _playerBaseSpeed;
    public float PlayerBaseHealth => _playerBaseHealth;
    public float PlayerBaseArmor => _playerBaseArmor;
    public float EnemyBaseSpeed => _enemyBaseSpeed;
    public float EnemyBaseHealth => _enemyBaseHealth;
    public float EnemyBaseArmor => _enemyBaseArmor;

    public List<GameObject> WeaponList()
    {
        return _weaponList;
    } 
    
    public GameObject WeaponList(int weaponIndex)
    {
        return _weaponList[weaponIndex];
    }

    public GameObject PlayerPrefabs()
    {
        return _playerPrefabs;
    }   
    
    public GameObject EnemyPrefabs()
    {
        return _enemyPrefabs;
    } 
    public GameObject HealthSliderPrefabs()
    {
        return _healthSliderPrefabs;
    }
    public GameObject ArmorSliderPrefabs()
    {
        return _armorSliderPrefabs;
    }
    public List<GameObject> WeaponUpgradePrefabs()
    {
        return _weaponUpgradePrefabs;
    }

    public GameObject WeaponUpgradePrefabs(int weaponUpgradeIndex)
    {
        return _weaponUpgradePrefabs[weaponUpgradeIndex];
    }
}
