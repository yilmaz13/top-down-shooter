using System.Collections.Generic;
using UnityEngine;

public class ShooterGameView : MonoBehaviour
{
    #region Private Members
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private List<Transform> _enemySpawnPoints;
    [SerializeField] private List<Transform> _weaponUpgradeSpawnPoints;

    private IShooterGameViewListener _listener;
    private Camera _gameCamera;
    private GameResources _gameResources;

    #endregion

    #region Public Members
    public string CurrentState { get; set; }
    public string TransitionState { get; set; }
    public Transform PlayerSpawnPoint => _playerSpawnPoint;
    public List<Transform> EnemySpawnPoints => _enemySpawnPoints;
    public List<Transform> WeaponUpgradeSpawnPoints => _weaponUpgradeSpawnPoints;

    #endregion

    public void Initialize(IShooterGameViewListener listener, Camera gameCamera, GameResources gameResources)
    {
        _listener = listener;
        _gameCamera = gameCamera;
        _gameResources = gameResources;
    }

    public void Clear()
    {
       
    }

    public void Create()
    {
        
    }

    public void Show()
    {
       gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
