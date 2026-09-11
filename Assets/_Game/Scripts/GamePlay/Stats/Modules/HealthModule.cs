using System;
using _Game.Scripts.GamePlay.Types;
using R3;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Modules
{
public class HealthModule : StatModule
{
    public ReadOnlyReactiveProperty<float> Current => _current;
    public ReadOnlyReactiveProperty<float> Max => _max;
    
    private readonly ReactiveProperty<float> _current = new();
    private readonly ReactiveProperty<float> _max = new();
    public bool HasMaxHp => Mathf.Approximately(_current.Value, _max.Value);
    
    public event Action<HealthModule> OnDeath;
    public event Action<HealthModule> OnRevived;
    public event Action<float> OnDamageTaken;
    public event Action OnHitTaken;
    public event Action<float> OnHealed;
    
    private bool _isDead;
    private float _extraLives;
    private float _extraLivesUsed;

    protected override void Configure()
    {
        BindStat(StatType.MaxHealth, UpdateMaxHealth);
        BindStat(StatType.ExtraLife, UpdateExtraLife);
    }

    private void Revive()
    {
        _isDead = false;
        _current.Value = _max.Value;
        _extraLivesUsed++;
        _extraLives--;
        OnRevived?.Invoke(this);
    }
    
    public void TakeDamage(float amount)
    {
        if (_isDead) return;
        _current.Value -= amount;
        _current.Value = Mathf.Max(0, _current.Value);
        OnHitTaken?.Invoke();
        if (amount > 0)
        {
            OnDamageTaken?.Invoke(amount);
        }
        
        if (_current.Value <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        var health = _current.Value;
        _current.Value += amount;
        if (_current.Value > _max.Value)
        {
            _current.Value = _max.Value;
        }

        if (Mathf.Approximately(health, _current.Value)) return;
        OnHealed?.Invoke(amount);
    }
    
    private void UpdateMaxHealth(float newMaxHealth)
    {
        var difference = newMaxHealth - _max.Value;
        _max.Value = newMaxHealth;
    
        _current.Value = Mathf.Clamp(_current.Value + difference, 0, _max.Value);
    }
    private void UpdateExtraLife(float value) => _extraLives = value - _extraLivesUsed;

    private void Die()
    {
        if (_isDead) return;
        _isDead = true;
        if (_extraLives > 0f)
        {
            Revive();
        }
        else
        {
            OnDeath?.Invoke(this);
        }
    }
}
}