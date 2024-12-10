using UnityEngine;

public class EnemyController : AgentController,
                               IDamageable
{
    #region Private Members

    [SerializeField] private Weapon _weapon;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _player;
    [SerializeField] private float _chaseRange = 10f;
    [SerializeField] private float _attackRange = 5f;
    [SerializeField] private float _attackCooldown = 1f;

    private IEnemyListener _listener;
    private float _lastAttackTime;
    private EnemyView _enemyView;

    #endregion

    #region Public Members
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
    public void Initialize(IEnemyListener listener, EnemyView enemyView, Transform player,
                           float speed, float health, float armor)
    {
        _enemyView = enemyView;
        _player = player;
        _listener = listener;

        base.Initialize(enemyView, speed, health, armor);
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
    
    protected override void OnDead()
    {
        _isActive = false;
        _listener.OnEnemyDead(this);
        _enemyView.OnDead();
        UnsubscribeHealthEvents();
    }

    #endregion
}
