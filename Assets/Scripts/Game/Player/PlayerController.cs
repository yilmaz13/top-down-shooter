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

    #endregion
    public float PlayerSpeed => _playerBaseSpeed;

    #region Public Methods

    public void Initialize(IPlayerController listener, PlayerView playerView, float playerBaseSpeed)
    {
        InitializeHealthAndArmorController(100, 100);

        _playerView = playerView;
        InitializeView();
        _isActive = true;
        HealthController.OnDead += Dead;
        _listener = listener;
    }

    void FixedUpdate()
    {
        if (!_isActive)
            return;

        if (_playerView == null)
            return;
        
        _playerView.Move();
        _playerView.LookAtMouse();
        _playerView.TurnSlidersAtCamera();
    }
    public void ApplyDamage(float damage, float armorPenetration)
    {
        TakeDamage(damage, armorPenetration);
        _playerView.UpdateHealthBar(HealthController.Health, HealthController.MaxHealth);
        _playerView.UpdateArmorBar(ArmorController.Armor, ArmorController.MaxArmor);
    }

    public void InitializeView()
    {      
        _playerView.InitializeHealthBar(HealthController.Health, HealthController.MaxHealth);
        _playerView.InitializeArmorBar(ArmorController.Armor, ArmorController.MaxArmor);
    }
    public Transform GetTransform()
    {
        return transform;
    }

    public void InitializeHealthAndArmorController(float maxHealth, float maxArmor)
    {
        HealthController = new HealthController();
        ArmorController = new ArmorController();

        HealthController.Initialize(maxHealth);
        ArmorController.Initialize(maxArmor);        
    }

    public void TakeDamage(float damage, float armorPenetration)
    {
        float remainingDamage = ArmorController.AbsorbDamage(damage, armorPenetration);
        HealthController.TakeDamage(remainingDamage);
    }

    private void Dead()
    {
        _isActive = false;
        _playerView.Dead();

        //TODO event sisteminde bir sorun var null referance hatasý alýyorum _listener ile haber kuruyorum
        //GameEvents.PlayerDead();
        _listener.PlayerDead();
    }

    public void Respawn(Vector3 spawnPosition)
    {
        _isActive = true;
        HealthController.Initialize(100);
        HealthController.OnDead += Dead;

        ArmorController.Initialize(100);
        _playerView.Transfer(spawnPosition);
        _playerView.Respawn();
    }

   #endregion
}
