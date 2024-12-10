using UnityEngine;

public class EnemyController : MonoBehaviour,
                               IDamageable
{
    #region Private Members

    [SerializeField] private Weapon _weapon;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _player;
    [SerializeField] private float _chaseRange = 10f;
    [SerializeField] private float _attackRange = 5f;
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private bool _isActive;

    private IEnemyListener _listener;
    private float _lastAttackTime;
    private EnemyView _enemyView;

    #endregion

    #region Public Members

    public HealthController HealthController;
    public ArmorController ArmorController;
    public EnemyView EnemyView => _enemyView;
    #endregion

    #region Unity Methods

    private void Awake()
    {
        _weapon.firePoint = _firePoint;
        _weapon.Initialize();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        if (distanceToPlayer <= _attackRange)
        {
            AttackPlayer();
        }
        else if (distanceToPlayer <= _chaseRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        _enemyView.TurnSlidersAtCamera();
    }
    #endregion

    #region Public Methods
    public void Initialize(Transform player, EnemyView enemyView, IEnemyListener listener)
    {
        _enemyView = enemyView;
        _player = player;
        _listener = listener;

        InitializeHealthAndArmorController(100, 0);
        InitializeView();

        SubscribeHealthEvents();
        _isActive = true;
    }

    public void ApplyDamage(float damage, float armorPenetration = 0)
    {
        TakeDamage(damage, armorPenetration);
        _enemyView.UpdateHealthBar(HealthController.Value, HealthController.MaxValue);
        _enemyView.UpdateArmorBar(ArmorController.Value, ArmorController.MaxValue);
    }
    #endregion

    #region Private Methods
    private void ChasePlayer()
    {
        //_navMeshAgent.SetDestination(_player.position);
    }

    private void AttackPlayer()
    {
        // _navMeshAgent.SetDestination(transform.position);
       
        _enemyView.LookAtPlayer(_player);

        if (Time.time >= _lastAttackTime + _attackCooldown)
        {
            _weapon.TryShoot(Owner.Enemy);
            _lastAttackTime = Time.time;
        }
    }

    private void Patrol()
    {
    }

    private void InitializeView()
    {
        _enemyView.InitializeHealthBar(HealthController.Value, HealthController.MaxValue);
        _enemyView.InitializeArmorBar(ArmorController.Value, ArmorController.MaxValue);
    }

    private void InitializeHealthAndArmorController(float maxHealth, float maxArmor)
    {
        HealthController = new HealthController();
        ArmorController = new ArmorController();

        HealthController.Initialize(maxHealth, OnDead);
        ArmorController.Initialize(maxArmor);
    }

    private void TakeDamage(float damage, float armorPenetration)
    {
        float remainingDamage = ArmorController.AbsorbDamage(damage, armorPenetration);
        HealthController.TakeDamage(remainingDamage);
    }
    private void OnDead()
    {
        _isActive = false;
        _listener.OnEnemyDead(this);
        _enemyView.OnDead();
        UnsubscribeHealthEvents();
    }

    private void SubscribeHealthEvents()
    {
        HealthController.OnDead += OnDead;
    }

    private void UnsubscribeHealthEvents()
    {
        HealthController.OnDead -= OnDead;
    }

    #endregion
}
