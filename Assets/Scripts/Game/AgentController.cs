using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentController : MonoBehaviour                               
{
    #region Private Members
    
    protected AgentView _view;
    protected bool _isActive;
    protected float _baseSpeed;
    protected float _baseHealth;
    protected float _baseArmor;
    #endregion

    #region Private Members

    public HealthController HealthController;
    public ArmorController ArmorController;
    public float Speed => _baseSpeed;

    #endregion

    public void Initialize(AgentView view, float baseSpeed, float health, float armor)
    {
        _view = view;
        _baseArmor = armor;
        _baseHealth = health;
        _baseSpeed = baseSpeed;

        InitializeHealthAndArmorController(health, armor);
        SubscribeHealthEvents();
        InitializeView();

        _isActive = true;
    }

    protected void InitializeHealthAndArmorController(float maxHealth, float maxArmor)
    {
        HealthController = new HealthController();
        ArmorController = new ArmorController();

        HealthController.Initialize(maxHealth, OnDead);
        ArmorController.Initialize(maxArmor);
    }

    protected void InitializeView()
    {
        _view.InitializeHealthBar(HealthController.Value, HealthController.MaxValue);
        _view.InitializeArmorBar(ArmorController.Value, ArmorController.MaxValue);
    }

    public void ApplyDamage(float damage, float armorPenetration)
    {
        TakeDamage(damage, armorPenetration);
        UpdateViewBars();
    }

    public void TakeDamage(float damage, float armorPenetration)
    {
        float remainingDamage = ArmorController.AbsorbDamage(damage, armorPenetration);
        HealthController.TakeDamage(remainingDamage);
    }
    public Transform GetTransform()
    {
        return transform;
    }

    protected void UpdateViewBars()
    {
        _view.UpdateHealthBar(HealthController.Value, HealthController.MaxValue);
        _view.UpdateArmorBar(ArmorController.Value, ArmorController.MaxValue);
    }
    
    protected void SubscribeHealthEvents()
    {
        HealthController.OnDead += OnDead;
    }

    protected void UnsubscribeHealthEvents()
    {
        HealthController.OnDead -= OnDead;
    }
    protected abstract void OnDead();
}
