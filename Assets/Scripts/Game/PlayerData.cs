using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private float _playerBaseSpeed;

    public float PlayerSpeed => _playerBaseSpeed;

    public void SetPlayerSpeed(float playerBaseSpeed) 
    {  
        _playerBaseSpeed = playerBaseSpeed; 
    }
}
