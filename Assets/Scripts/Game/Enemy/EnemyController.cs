using UnityEngine;
using UnityEngine.AI;

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
    private float _lastAttackTime;
    private EnemyView _enemyView;

    #endregion

    #region Public Members

    public HealthController HealthController;
    public ArmorController ArmorController;

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
    }
    #endregion

    #region Public Methods
    public void Initialize(Transform player, EnemyView enemyView)
    {
        _enemyView = enemyView;
         _player = player;

        InitializeHealthAndArmorController(100, 0);
        InitializeView();

        HealthController.OnDead += Dead;
    }
    public void ApplyDamage(float damage, float armorPenetration = 0)
    {
        TakeDamage(damage, armorPenetration);
        _enemyView.UpdateHealthBar(HealthController.Health, HealthController.MaxHealth);
        _enemyView.UpdateArmorBar(ArmorController.Armor, ArmorController.MaxArmor);
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
        transform.LookAt(_player);

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
        _enemyView.InitializeHealthBar(HealthController.Health, HealthController.MaxHealth);
        _enemyView.InitializeArmorBar(ArmorController.Armor, ArmorController.MaxArmor);
    }
   
    private void InitializeHealthAndArmorController(float maxHealth, float maxArmor)
    {
        HealthController = new HealthController();
        ArmorController = new ArmorController();

        HealthController.Initialize(maxHealth);
        ArmorController.Initialize(maxArmor);
    }

    private void TakeDamage(float damage, float armorPenetration)
    {
        float remainingDamage = ArmorController.AbsorbDamage(damage, armorPenetration);
        HealthController.TakeDamage(remainingDamage);
    }
    private void Dead()
    {
        _isActive = false;
        _enemyView.Dead();
    }
    #endregion
}
