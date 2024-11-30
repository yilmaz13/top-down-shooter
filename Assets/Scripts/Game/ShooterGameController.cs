using UnityEngine;

public class ShooterGameController : IShooterGameViewListener
{
    #region Private Members    

    private ShooterGameData _data;
    private ShooterGameView _view;
    private GameUIView _gameUIView;
    private IShooterGameListener _listener;
    private GameResources _gameResources;

    private PlayerController _playerController;

    //  private EnemyController _enemyController;
    private Camera _camera;

    #endregion

    //  CONSTRUCTION
    public ShooterGameController()
    {
        _data = new ShooterGameData();
    }

    public void Initialize(ShooterGameView view, GameUIView gameUIView, IShooterGameListener listener, GameResources gameResources, Camera _camera)
    {
        _listener = listener;
        _gameResources = gameResources;
        _gameUIView = gameUIView;
        _view = view;
    }

    #region Private Methods    
    private void SpawnPlayer()
    {
        GameObject playerObject = GameObject.Instantiate(_gameResources.PlayerPrefabs(), _view.PlayerSpawnPoint);
        var playerView = playerObject.GetComponent<PlayerView>();

        _playerController = playerObject.GetComponent<PlayerController>();
        _playerController.Initialize(playerView, _gameResources.PlayerBaseSpeed);

        GameEvents.SpawnedPlayer(playerView.transform);
    }
    #endregion

    #region Public Methods    
    public void Load(Level _level)
    {
        _data.Load();
        _view.Create();

        SpawnPlayer();
    }
    public void Unload()
    {
        _data.Unload();
        _view.Clear();
    }  

    #endregion

}
