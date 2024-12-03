using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour,
                               IDamageable 
{    
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _player;
    [SerializeField] private float _chaseRange = 10f;
    [SerializeField] private float _attackRange = 5f;
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private bool _isActive;

    private EnemyView _enemyView;

    public HealthController HealthController;
    public ArmorController ArmorController;

    private float _lastAttackTime;

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

    public void Initialize(Transform player, EnemyView enemyView)
    {
        // _weapon = weapon;
        _enemyView = enemyView;
         _player = player;
        InitializeHealthAndArmorController(100, 0);
        InitializeView();
        HealthController.OnDead += Dead;
    }
    private void ChasePlayer()
    {
        //_navMeshAgent.SetDestination(_player.position);
    }

    private void AttackPlayer()
    {
       // _navMeshAgent.SetDestination(transform.position); // Düþmaný durdur
        transform.LookAt(_player);

        if (Time.time >= _lastAttackTime + _attackCooldown)
        {
            _weapon.Shoot(Owner.Enemy);
            _lastAttackTime = Time.time;
        }
    }

    private void Patrol()
    {
        // Devriye gezme davranýþý burada uygulanabilir
    }
  
    public void ApplyDamage(float damage, float armorPenetration = 0)
    {
        TakeDamage(damage, armorPenetration);
        _enemyView.UpdateHealthBar(HealthController.Health, HealthController.MaxHealth);
        _enemyView.UpdateArmorBar(ArmorController.Armor, ArmorController.MaxArmor);    
    }
    public void InitializeView()
    {
        _enemyView.InitializeHealthBar(HealthController.Health, HealthController.MaxHealth);
        _enemyView.InitializeArmorBar(ArmorController.Armor, ArmorController.MaxArmor);
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
        _enemyView.Dead();
    }
}
