
using System;

public class HealthController : BaseValueController
{
    public Action OnDead;

    #region Public Methods

    public void Initialize(float value, Action onDead)
    {
        base.Initialize(value);
        OnDead = onDead;
    }
    public void TakeDamage(float damage)
    {
        _value -= damage;
        if (_value < 0)
        {
            Dead();
        }
    }

    #endregion

    #region Private Methods
    private void Dead()
    {
        _value = 0;
        OnDead.Invoke();
    }

    #endregion
}
