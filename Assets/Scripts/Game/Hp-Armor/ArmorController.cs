public class ArmorController
{
    #region Private Members
    private float _armor;
    private float _maxArmor;

    #endregion

    #region Public Members
    public float Armor => _armor;
    public float MaxArmor => _maxArmor;

    #endregion

    #region Public Methods

    public void Initialize(float armor)
    {
        _armor = armor;
        _maxArmor = armor;
    }
    public float AbsorbDamage(float damage, float armorPenetration)
    {
        float damageToArmor = damage * ((100 - armorPenetration) / 100);
        float remainingDamage = damage - damageToArmor;

        if (_armor > 0)
        {
            if (_armor >= damageToArmor)
            {
                _armor -= damageToArmor;
            }
            else
            {
                remainingDamage += (damageToArmor - _armor);
                _armor = 0;
            }
        }

        return remainingDamage;
    }

    public void RepairArmor(float amount)
    {
        _armor += amount;
        if (_armor > 100)
        {
            _armor = 100;
        }
    }

    #endregion
}
