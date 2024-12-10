using UnityEngine;

public class PlayerController : MonoBehaviour,
                                IDamageable
{
    #region Private Members

    private IPlayerController _listener;
    private PlayerView _playerView;
    private bool _isActive;
    private float _playerBaseSpeed;

    #endregion

    #region Private Members

    public HealthController HealthController;
    public ArmorController ArmorController;
    public float PlayerSpeed => _playerBaseSpeed;

    #endregion

    #region Public Methods

    public void Initialize(IPlayerController listener, PlayerView playerView, float playerBaseSpeed)
    {      
        _playerView = playerView;                
        _listener = listener;
        _playerBaseSpeed = playerBaseSpeed;

        //TODO get value from game resources
        InitializeHealthAndArmorController(100, 100);
        SubscribeToHealthControllerEvents();
        InitializeView();

        _isActive = true;
    }

    void FixedUpdate()
    {
        if (!_isActive || _playerView == null)
            return;

        _playerView.Move();
        _playerView.LookAtMouse();
        _playerView.TurnSlidersAtCamera();
    }
    public void ApplyDamage(float damage, float armorPenetration)
    {
        TakeDamage(damage, armorPenetration);
        UpdateViewBars();     
    }
    
    public Transform GetTransform()
    {
        return transform;
    }  

    public void TakeDamage(float damage, float armorPenetration)
    {
        float remainingDamage = ArmorController.AbsorbDamage(damage, armorPenetration);
        HealthController.TakeDamage(remainingDamage);
    }

    #endregion

    #region Private Methods

    private void InitializeHealthAndArmorController(float maxHealth, float maxArmor)
    {
        HealthController = new HealthController();
        ArmorController = new ArmorController();

        HealthController.Initialize(maxHealth, OnDead);
        ArmorController.Initialize(maxArmor);
    }

    private void InitializeView()
    {
        _playerView.InitializeHealthBar(HealthController.Value, HealthController.MaxValue);
        _playerView.InitializeArmorBar(ArmorController.Value, ArmorController.MaxValue);
    }

    private void UpdateViewBars()
    {
        _playerView.UpdateHealthBar(HealthController.Value, HealthController.MaxValue);
        _playerView.UpdateArmorBar(ArmorController.Value, ArmorController.MaxValue);
    }

    private void OnDead()
    {
        _isActive = false;
        _playerView.OnDead();

        UnsubscribeFromHealthControllerEvents();

        GameEvents.PlayerDead();       
    }

    public void Respawn(Vector3 spawnPosition)
    {
        _isActive = true;
        //TODO get value from game resources
        InitializeHealthAndArmorController(100, 100);
        SubscribeToHealthControllerEvents();
        
        UpdateViewBars();
        _playerView.ShowBars();
        _playerView.Transfer(spawnPosition);
        _playerView.Respawn();
    }   

    private void SubscribeToHealthControllerEvents()
    {
        HealthController.OnDead += OnDead;
    }

    private void UnsubscribeFromHealthControllerEvents()
    {
        HealthController.OnDead -= OnDead;
    }

    private void OnDestroy()
    {
        //double check 
        UnsubscribeFromHealthControllerEvents();
    }
    #endregion
}
