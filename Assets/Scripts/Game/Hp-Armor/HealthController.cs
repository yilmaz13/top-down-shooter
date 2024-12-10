
using System;

public class HealthController : BaseValueController
{
    public Action OnDead;

    public bool isDead;
    #region Public Methods

    public void Initialize(float value, Action onDead)
    {
        base.Initialize(value);
        isDead = false;
        OnDead = onDead;
    }

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            _value -= damage;

            if (_value <= 0)
            {
                Dead();
            }
        }
    }

    #endregion

    #region Private Methods
    private void Dead()
    {
        _value = 0;
        isDead = true;
        OnDead.Invoke();
    }

    #endregion
}
