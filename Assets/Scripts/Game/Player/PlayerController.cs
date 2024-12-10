using UnityEngine;

public class PlayerController : AgentController,
                                IDamageable
{
    #region Private Members

    private IPlayerController _listener;
    private PlayerView _playerView;

    #endregion

    #region Public Methods

    public void Initialize(IPlayerController listener, PlayerView playerView, 
                           float playerBaseSpeed,
                           float playerBaseHealth,
                           float playerBaseArmor)
    {      
        _playerView = playerView;                
        _listener = listener;

        base.Initialize(playerView, playerBaseSpeed, playerBaseHealth, playerBaseArmor);
    }

    protected void FixedUpdate()
    {
        if (!_isActive || _playerView == null)
            return;

        _playerView.Move();
        _playerView.LookAtMouse();
        _playerView.TurnSlidersAtCamera();
    }  

    #endregion

    #region Private Methods       

    protected override void OnDead()
    {      
        if (_isActive)
        {
            _playerView.OnDead();
            UnsubscribeHealthEvents();
            //_listener.PlayerDead();
            GameEvents.PlayerDead();
            _isActive = false;
        }         
    }

    public void Respawn(Vector3 spawnPosition)
    {
        _isActive = true;
        //TODO get value from game resources
        InitializeHealthAndArmorController(_baseHealth, _baseArmor);
        SubscribeHealthEvents();
        
        UpdateViewBars();

        _playerView.ShowBars();
        _playerView.Transfer(spawnPosition);
        _playerView.Respawn();
    }

    protected void OnDestroy()
    {
        //double check 
        UnsubscribeHealthEvents();
    }
    #endregion
}
