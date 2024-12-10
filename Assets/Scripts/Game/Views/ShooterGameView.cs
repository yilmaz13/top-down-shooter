using System.Collections.Generic;
using UnityEngine;

public class ShooterGameView : MonoBehaviour
{
    #region Private Members
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private List<Transform> _enemySpawnPoints;
    [SerializeField] private List<Transform> _weaponUpgradeSpawnPoints;
    [SerializeField] private List<Transform> _enemyPatrolPath;

    private List<EnemyView> _enemyViews;
    private List<WeaponUpgradeCollectable> _weaponUpgradeViews;
    private PlayerView _playerView;
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

    public List<Transform> EnemyPatrolPath => _enemyPatrolPath;
    #endregion

    public void Initialize(IShooterGameViewListener listener, Camera gameCamera, GameResources gameResources)
    {
        _listener = listener;
        _gameCamera = gameCamera;
        _gameResources = gameResources;

        _enemyViews = new List<EnemyView>();
        _weaponUpgradeViews = new List<WeaponUpgradeCollectable>();
    }

    public void AddEnemyViews(EnemyView enemyView)
    {
        _enemyViews.Add(enemyView);
    }

    //TODO with id. dont use EnemyView
    public void RemoveEnemyViews(EnemyView enemyView)
    {
        _enemyViews.Remove(enemyView);
    }

    public void AddWeaponUpgradeViews(WeaponUpgradeCollectable weaponUpgradeView)
    {
        _weaponUpgradeViews.Add(weaponUpgradeView);
    }
    public void RemoveWeaponUpgradeViews(WeaponUpgradeCollectable weaponUpgradeView)
    {
        _weaponUpgradeViews.Remove(weaponUpgradeView);
    }
    public void SetPlayerView(PlayerView playerView)
    {
        _playerView = playerView;
    }
    public void RemovePlayerView()
    {
        _playerView = null;
    }

    public void Clear()
    {
        for (int i = 0; i < _enemyViews.Count; i++)
        {
            EnemyView enemyView = _enemyViews[i];
            //TODO pool
            Destroy(enemyView.gameObject, 0.1f);           
        }

        _enemyViews.Clear();

        for (int index = 0; index < _weaponUpgradeViews.Count; index++)
        {
            WeaponUpgradeCollectable weaponUpgradeView = _weaponUpgradeViews[index];
            //TODO pool
            Destroy(weaponUpgradeView.gameObject, 0.1f);           
        }

        _weaponUpgradeViews.Clear();

        Destroy(_playerView.gameObject, 0.1f);
    }    

    public void Create()
    {
        _enemyViews.Clear();
        _weaponUpgradeViews.Clear();
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
