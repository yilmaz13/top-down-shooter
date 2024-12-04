using System.Collections.Generic;
using UnityEngine;

public class GamePlayGameState : AStateBase,
                                 IShooterGameListener
{
    #region Private Members    

    IStateManager _stateManager;
    IUserDataManager _userDataManager;
    SceneReferences _sceneReferences;
    ResourceReferences _resourceReferences;

    private Camera _camera;
    private GameUIView _gameUIView;
    private ShooterGameView _shooterGameView;
    private ShooterGameController _shooterController;
    private CameraController _cameraController;

    private GameResources _gameResources;
    private Level _level;
    private bool _startedGame;

    //Pool

    private ProjectilePool _projectilePool;

    #endregion

    //  CONSTRUCTION
    public GamePlayGameState(IStateManager      stateManager,
                             IUserDataManager   userDataManager,   
                             SceneReferences    sceneReferences,
                             ResourceReferences resourceReferences) : base(StateNames.Game)
    {
        _stateManager = stateManager;
        _userDataManager = userDataManager;
        _sceneReferences = sceneReferences;
        _resourceReferences = resourceReferences;
        _gameResources = _resourceReferences.GameResources;
    }

    #region State Methos
    public override void Activate()
    {
        Debug.Log("<color=green>GameplayGame State</color> OnActive");

        SpawnViewandControler();
        
        _shooterGameView.Show();

        SubscribeEvents();

        InitializeCamera();

        PlayLevel();
    }

    public override void Deactivate()
    {
        Debug.Log("<color=red>GameplayGame State</color> DeOnActive");

        UnsubscribeEvents();
    }

    public override void UpdateState()
    {        
    }

    #endregion

    #region Private Methos
    private void SpawnViewandControler()
    {
        _camera = _sceneReferences.MainCam;
        if (_shooterGameView == null)
        {
            GameObject gameUIViewObject = GameObject.Instantiate(_resourceReferences.GameUIPrefab, _sceneReferences.UIViewContainer.transform);
            _gameUIView = gameUIViewObject.GetComponent<GameUIView>();

            _shooterController = new ShooterGameController();
            GameObject mainMenuObject = GameObject.Instantiate(_resourceReferences.GameViewPrefab, _sceneReferences.ViewContainer.transform);

            _shooterGameView = mainMenuObject.GetComponent<ShooterGameView>();
            _shooterGameView.Initialize(_shooterController, Camera.main, _resourceReferences.GameResources);

            _shooterController.Initialize(_shooterGameView, _gameUIView, this, _resourceReferences.GameResources, _camera);
        }

        if (_projectilePool == null)
        {
            GameObject projectilePool = GameObject.Instantiate(_resourceReferences.ProjectilePoolPrefab, _sceneReferences.PoolContainer.transform);
            _projectilePool = projectilePool.GetComponent<ProjectilePool>();            
        }
    }

    private void InitializeCamera()
    {
        _cameraController = _sceneReferences.MainCam.GetComponent<CameraController>();       
    }

    private void FollowCameraPlayer(Transform playerTranfrom)
    {
        _cameraController.Initialize(playerTranfrom, _gameResources.FollowCameraSmootTime);
    }

    private void PlayLevel()
    {
        int index = _userDataManager.CurrentLevel();

        _level = ScriptableObject.CreateInstance<Level>();
        var currentLevelTemplate = Resources.Load<Level>("Level" + index);
        _level.CopyLevel(_level, currentLevelTemplate);

        _shooterController.Load(_level);
        
        _startedGame = true;

        _gameUIView.LoadLevel(index);
        _gameUIView.Show();        
    }
    private void SubscribeEvents()
    {
        GameEvents.OnStartGame += StartGameListener;
        GameEvents.OnEndGame += EndGameListener;    
        GameEvents.OnClickGotoMenu += GotoMenu;      

        GameEvents.OnSpawnedPlayer += FollowCameraPlayer;
    }

    private void UnsubscribeEvents()
    {
        GameEvents.OnStartGame -= StartGameListener;
        GameEvents.OnEndGame -= EndGameListener;     
        GameEvents.OnClickGotoMenu -= GotoMenu;     

        GameEvents.OnSpawnedPlayer -= FollowCameraPlayer;
    }

    private void ClearScene()
    {
        //Hide Popups;
        _gameUIView.UnloadLevel();
        _gameUIView.Hide();

        _shooterController.Unload();       
    }
    private void GotoMenu()
    {
        ClearScene();
        _stateManager.ChangeTransitionState(StateNames.Loading, StateNames.MainMenu);
    }

    private void EndGameListener(bool success)
    {
        if (!_startedGame)
            return;

        if (success)
        {
            //LevelSuccess
            _startedGame = false;
        }
        else
        {
            //LevelFail
            _startedGame = false;
        }
    }

    private void StartGameListener()
    {

    }
    #endregion
}
