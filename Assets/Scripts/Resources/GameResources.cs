using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameResources", menuName = "ScriptableObjects/GameResources", order = 0)]

public class GameResources : ScriptableObject
{
    [Header("Config")]
    
    [SerializeField] private List<GameObject> _weaponList;

    public List<GameObject> WeaponList()
    {
        return _weaponList;
    } 
    
    public GameObject WeaponList(int weaponIndex)
    {
        return _weaponList[weaponIndex];
    }
}
