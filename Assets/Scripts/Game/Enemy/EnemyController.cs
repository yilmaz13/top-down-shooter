using UnityEngine;

public class EnemyController : AgentController,
                               IDamageable
{
    #region Private Members

    [SerializeField] private Weapon _weapon;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _player;
    [SerializeField] private float _chaseRange;
    [SerializeField] private float _attackRange;


    private IEnemyListener _listener;
    private EnemyView _enemyView;

    private Transform _patrolPathParent;
    private int patrolIndex = 0;
    private bool _hasPatrol;
    private Vector3 _currentPatrol;
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
        if (_player == null) return;

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

        _enemyView?.TurnSlidersAtCamera();
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
        _enemyView.Move(_player.position);
        _hasPatrol = false;
    }

    private void AttackPlayer()
    {
        if (_player == null)
            return;

        _enemyView.LookAtPlayer(_player);

        if (_weapon.CanShoot())
        {
            _weapon.TryShoot(Owner.Enemy);           
        }

        _hasPatrol = false;
    }

    private void Patrol()
    {
        if (!_hasPatrol)
        {
            patrolIndex = 0;
            _currentPatrol = _patrolPathParent.GetChild(patrolIndex).position;
            _hasPatrol = true;
             _enemyView.Move(_currentPatrol);
        }
        else if ((Vector3.Distance(transform.position, _currentPatrol)) < 1.1f)
        {
            MoveNextPatrolPoint();
        }
    }
   
    public void SetPatrolPoints(Transform patrolPathParent)
    {
        _patrolPathParent = patrolPathParent;
    }

    public void MoveNextPatrolPoint()
    {
        patrolIndex++;
        if (_patrolPathParent.childCount <= patrolIndex)
            patrolIndex = 0;

        _currentPatrol = _patrolPathParent.GetChild(patrolIndex).position;        
        _enemyView.Move(_currentPatrol);
    }

    protected override void OnDead()
    {
        _isActive = false;
        _listener.OnEnemyDead(this);
        _enemyView.OnDead();
        UnsubscribeHealthEvents();      
    }

    private void OnDestroy()
    {       
    }
    #endregion
}
