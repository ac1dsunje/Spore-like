using System;
using _Game.Scripts.GamePlay.Types;
using _Game.Scripts.GamePlay.UI.Bar;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Modules
{
public class HealthModule: StatModule, IResource
{
    public float MaxHealth {get;  private set; }
    public float Health { get; private set; }
    public float Regeneration { get; private set; }
    public float ExtraLives { get; private set; }
    
    public event Action<HealthModule> OnDeath;
    public event Action<HealthModule> OnRevived;
    public event Action<float> OnDamageTaken;
    public event Action OnHitTaken;
    public event Action<float> OnHealed;
    public event Action<float, float> OnValueChanged;
    
    private bool _isDead;
    private float _extraLivesUsed;

    protected override void Configure()
    {
        BindStat(StatType.MaxHealth, UpdateMaxHealth);
        BindStat(StatType.Regeneration, UpdateRegeneration);
        BindStat(StatType.ExtraLife, UpdateExtraLife);
    }

    private void Revive()
    {
        OnRevived?.Invoke(this);
        _isDead = false;
        Health = MaxHealth;
        _extraLivesUsed++;
        ExtraLives--;
    }
    
    public void TakeDamage(float amount)
    {
        if (_isDead) return;
        Health -= amount;
        Health = Mathf.Max(0, Health);
        OnHitTaken?.Invoke();
        if (amount > 0)
        {
            OnDamageTaken?.Invoke(amount);
            OnValueChanged?.Invoke(Health, MaxHealth);
        }
        
        if (Health <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        var health = Health;
        Health += amount;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }

        if (Mathf.Approximately(health, Health)) return;
        OnHealed?.Invoke(amount);
        OnValueChanged?.Invoke(Health, MaxHealth);
    }
    
    private void UpdateMaxHealth(float newMaxHealth)
    {
        var difference = newMaxHealth - MaxHealth;
        MaxHealth = newMaxHealth;
    
        Health = Mathf.Clamp(Health + difference, 0, MaxHealth);
    
        OnValueChanged?.Invoke(Health, MaxHealth);
    }

    private void UpdateRegeneration(float value) => Regeneration = value;
    private void UpdateExtraLife(float value) => ExtraLives = value - _extraLivesUsed;

    private void Die()
    {
        if (_isDead) return;
        _isDead = true;
        if (ExtraLives > 0f)
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