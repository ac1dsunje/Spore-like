using System;
using UnityEngine;

namespace _Game.Scripts.Player.Modules.Health
{
public class HealthStats
{
    public float MaxHealth {get;  private set; }
    public float Health { get; private set; }
    public float Regeneration { get; private set; }
    
    public event Action OnDeath;
    public event Action OnDamageTaken;
    public event Action<float, float> OnHealthChanged;

    public void Initialize(float maxHealth)
    {
        MaxHealth = maxHealth;
        Health = maxHealth;
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
    
    public void UpdateMaxHealth(float newMaxHealth)
    {
        var difference = newMaxHealth - MaxHealth;
        MaxHealth = newMaxHealth;
    
        Health = Mathf.Clamp(Health + difference, 0, MaxHealth);
    
        OnHealthChanged?.Invoke(Health, MaxHealth);
    }

    public void UpdateRegeneration(float newRegeneration)
    {
        Regeneration = newRegeneration;
    }
    
    private void Die()
    {
        OnDeath?.Invoke();
    }
}
}