using System;
using _Game.Scripts.Evolutions.Stats;
using UnityEngine;

namespace _Game.Scripts.Player.Modules.Health
{
public class HealthStats: IDisposable
{
    public float MaxHealth {get;  private set; }
    public float Health { get; private set; }
    public float Regeneration { get; private set; }
    
    public event Action OnDeath;
    public event Action OnDamageTaken;
    public event Action<float, float> OnHealthChanged;
    
    private PlayerStats _stats;

    public HealthStats(PlayerStats stats)
    {
        _stats.OnStatUpdated += OnStatUpdated;
    }

    private void OnStatUpdated(EvolutionType type, float value)
    {
        switch (type)
        {
            case EvolutionType.MaxHealth:
                UpdateMaxHealth(value);
                break;
            case EvolutionType.RegenerationSpeed:
                UpdateRegeneration(value);
                break;
        }
    }
    
    public void TakeDamage(float amount)
    {
        Health -= amount;
        Health = Mathf.Max(0, Health);
        OnDamageTaken?.Invoke();
        OnHealthChanged?.Invoke(Health, MaxHealth);
        
        if (Health <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        Health += amount;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        OnHealthChanged?.Invoke(Health, MaxHealth);
    }
    
    private void UpdateMaxHealth(float newMaxHealth)
    {
        var difference = newMaxHealth - MaxHealth;
        MaxHealth = newMaxHealth;
    
        Health = Mathf.Clamp(Health + difference, 0, MaxHealth);
    
        OnHealthChanged?.Invoke(Health, MaxHealth);
    }

    private void UpdateRegeneration(float newRegeneration)
    {
        Regeneration = newRegeneration;
    }
    
    private void Die()
    {
        OnDeath?.Invoke();
    }

    public void Dispose()
    {
        _stats.OnStatUpdated -= OnStatUpdated;
    }
}
}