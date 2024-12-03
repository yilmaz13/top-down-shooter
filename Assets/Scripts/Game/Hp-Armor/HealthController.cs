using System;

public class HealthController
{
    #region Private Members

    private float _health;
    private float _maxHealth;  

    #endregion

    #region Public Members

    public float Health => _health;
    public float MaxHealth => _maxHealth;
    public float HealthPercentage => _health / _maxHealth;
    public Action OnDead;
    #endregion

    #region Public Methods
    public void Initialize(float health, Action onDead = null)
    {
        _health    = health;
        _maxHealth = health;
        OnDead = onDead;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health < 0)
        {
            _health = 0;
            OnDead.Invoke();
            OnDead = null;
        }
    }

    public void Cure(float cureAmount)
    {
        _health += cureAmount;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }

    #endregion
}
