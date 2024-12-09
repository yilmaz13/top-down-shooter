using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class ShooterGameController : IShooterGameViewListener,
                                     IPlayerController
{
    #region Private Members    

    private ShooterGameData _data;
    private ShooterGameView _view;
    private GameUIView _gameUIView;
    private IShooterGameListener _listener;
    private GameResources _gameResources;

    private PlayerController _playerController;
    private List<EnemyController> _enemyController;
    private Camera _camera;

    public ShooterGameView View => _view;
    #endregion

    //  CONSTRUCTION
    public ShooterGameController()
    {
        _data = new ShooterGameData();
    }

    public void Initialize(ShooterGameView view, GameUIView gameUIView, IShooterGameListener listener, GameResources gameResources, Camera camera)
    {
        _listener = listener;
        _gameResources = gameResources;
        _gameUIView = gameUIView;
        _camera = camera;
        _view = view;
        _enemyController = new List<EnemyController>();

        SubscribeEvents();
    }

    #region Private Methods    
    private void SpawnPlayer()
    {
        GameObject playerObject = GameObject.Instantiate(_gameResources.PlayerPrefabs(), _view.PlayerSpawnPoint);
        var playerView = playerObject.GetComponent<PlayerView>();

        _playerController = playerObject.GetComponent<PlayerController>();
        _playerController.Initialize(this, playerView, _gameResources.PlayerBaseSpeed);
        playerView.Initialize(_gameResources.PlayerBaseSpeed, _camera);

     //   GameEvents.SpawnedPlayer(playerView.transform);
        _listener.FollowCameraPlayer(playerView.transform);
    }

    private void SpawnEnemy(Transform playerTransform)
    {
        for (int i = 0; i < _view.EnemySpawnPoints.Count; i++)
        {
            GameObject enemyObject = GameObject.Instantiate(_gameResources.EnemyPrefabs(), _view.EnemySpawnPoints[i]);

            var enemyController = enemyObject.GetComponent<EnemyController>();
            var enemyView = enemyObject.GetComponent<EnemyView>();
            enemyController.Initialize(playerTransform, enemyView);

            _enemyController.Add(enemyController);
        }
    }

    private void SpawnWeaponUpgrades()
    {
        var weaponPrefabs = _gameResources.WeaponUpgradePrefabs();
        foreach (var spawnPoint in _view.WeaponUpgradeSpawnPoints)
        {
            int randomIndex = Random.Range(0, weaponPrefabs.Count);
            GameObject upgradeObject = GameObject.Instantiate(_gameResources.WeaponUpgradePrefabs(randomIndex), spawnPoint.position, spawnPoint.rotation);
            
        }
    }

    private void SubscribeEvents()
    {
        GameEvents.OnPlayerDead += PlayerDead;
    }

    private void UnsubscribeEvents()
    {
        GameEvents.OnPlayerDead -= PlayerDead;
    }
    #endregion

    #region Public Methods    
    public void Load(Level _level)
    {
        _data.Load();
        _view.Create();

        SpawnPlayer();

        var _playerTransform = _playerController.GetTransform();
        SpawnEnemy(_playerTransform);
        SpawnWeaponUpgrades();
    }

    public void Unload()
    {
        _data.Unload();
        _view.Clear();
        _enemyController.Clear();
    }

    public void PlayerDead()
    {
        DOVirtual.DelayedCall(1,
           () => RespawnPlayer(_view.PlayerSpawnPoint.position));
    }

    public void RespawnPlayer(Vector3 spawnPosition)
    {
        _playerController.Respawn(spawnPosition);
    }
    #endregion
}
