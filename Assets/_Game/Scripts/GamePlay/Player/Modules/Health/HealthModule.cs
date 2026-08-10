using System;
using _Game.Scripts.GamePlay.Player.Modules.Stats;
using _Game.Scripts.GamePlay.Stats;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Player.Modules.Health
{
public class HealthModule: StatModule, IResource
{
    public float MaxHealth {get;  private set; }
    public float Health { get; private set; }
    public float Regeneration { get; private set; }
    
    public event Action OnDeath;
    public event Action<float> OnDamageTaken;
    public event Action<float> OnHealed;
    public event Action<float, float> OnValueChanged;

    public HealthModule(PlayerStats playerStats) : base(playerStats)
    {
        BindStat(StatType.MaxHealth, UpdateMaxHealth);
        BindStat(StatType.Regeneration, UpdateRegeneration);
    }
    
    public void TakeDamage(float amount)
    {
        Health -= amount;
        Health = Mathf.Max(0, Health);
        OnDamageTaken?.Invoke(amount);
        OnValueChanged?.Invoke(Health, MaxHealth);
        
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

    private void Die()
    {
        OnDeath?.Invoke();
    }
}
}