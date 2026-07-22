using System;
using System.Collections.Generic;
using _Game.Scripts.Evolutions;
using _Game.Scripts.Evolutions.Stats;
using _Game.Scripts.Player.Modules.Vision;
using _Game.Scripts.World.Food;

namespace _Game.Scripts.Player
{
public class PlayerStats
{
    public float MoveSpeed => _stats.GetValueOrDefault(EvolutionType.MoveSpeed);
    public float Acceleration => _stats.GetValueOrDefault(EvolutionType.Acceleration);
    public float DamageReflection => _stats.GetValueOrDefault(EvolutionType.DamageReflection);
    public float EatingSpeed => _stats.GetValueOrDefault(EvolutionType.EatingSpeed);
    public float PhysicalDamage => _stats.GetValueOrDefault(EvolutionType.PhysicalDamage);
    public float RegenerationSpeed => _stats.GetValueOrDefault(EvolutionType.RegenerationSpeed);
    public float Inertia => _stats.GetValueOrDefault(EvolutionType.Inertia);
    public float Stamina => _stats.GetValueOrDefault(EvolutionType.Stamina);

    public event Action<Stat> OnStatUpdated;
    public event Action<FoodItem> OnFoodEaten;
    
    // Vision
    
    public VisionStats  Vision { get; } = new();
    
    // Health
    public float MaxHealth => _stats.GetValueOrDefault(EvolutionType.MaxHealth);
    public float Health { get; private set; }
    
    public event Action OnDeath;
    public event Action OnDamageTaken;
    public event Action<float, float> OnHealthChanged;

    //Level
    private int _levelSet;
    private int _experience;
    private int _level;
    private int _levelScaler;
    
    public event Action<int> OnExperienceChanged;
    public event Action<int> OnExperienceGained;
    public event Action<int> OnLevelChanged;
    
    //Evolutions
    private readonly List<Evolution> _evolutions = new();
    public event Action<Evolution> OnEvolutionAdded;
    //Stats
    private readonly Dictionary<EvolutionType, float> _stats = new();

    public PlayerStats(PlayerConfig config)
    {
        AddStats(config.Stats);
        
        _levelSet = config.ExperienceConfig.LevelSet;
        _levelScaler = config.ExperienceConfig.LevelScaler;
        Health = MaxHealth;

        Vision.UpdateRadius(_stats.GetValueOrDefault(EvolutionType.VisionRadius));
    }

    public void Eat(int amount, FoodItem food)
    {
        AddExperience(amount);
        OnFoodEaten?.Invoke(food);

        food.Release();
    }

    private void AddExperience(int amount)
    {
        OnExperienceGained?.Invoke(amount);
        UpdateExperience(amount);
    }

    private void UpdateExperience(int amount)
    {
        _experience += amount;
        OnExperienceChanged?.Invoke(_experience);
        UpdateLevel();
    }
    
    private void UpdateLevel()
    {
        while (_experience >= _levelSet)
        {
            UpdateExperience(-_levelSet);
            _level++;
            OnLevelChanged?.Invoke(_level);
            _levelSet += _levelScaler;
            _levelScaler++;
        }
    }

    public void AddEvolution(Evolution evolution)
    {
        _evolutions.Add(evolution);
        OnEvolutionAdded?.Invoke(evolution);
        
        AddStats(evolution.Stats);
    }

    public float TakeDamage(float damage)
    {
        LowerHealth(damage);
        return ReturnDamage(damage);
    }

    private void LowerHealth(float amount)
    {
        Health -= amount;
        OnDamageTaken?.Invoke();
        OnHealthChanged?.Invoke(Health, MaxHealth);
        
        if (Health <= 0)
        {
            Die();
        }
    }

    private float ReturnDamage(float damage)
    {
        return damage * DamageReflection / 100;
    }

    private void Die()
    {
        OnDeath?.Invoke();
    }

    private void AddStats(List<Stat> stats)
    {
        foreach (var stat in stats)
        {
            if (!_stats.ContainsKey(stat.Type))
            {
                _stats.Add(stat.Type, stat.Value);
            }
            else
            {
                _stats[stat.Type] *= 1 + stat.Value / 100f;
            }

            if (stat.Type == EvolutionType.VisionRadius)
            {
                Vision.UpdateRadius(_stats[EvolutionType.VisionRadius]);
            }

            OnStatUpdated?.Invoke(stat);
        }
    }
}
}