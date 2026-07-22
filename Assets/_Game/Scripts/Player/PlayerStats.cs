using System;
using System.Collections.Generic;
using _Game.Scripts.Evolutions;
using _Game.Scripts.Evolutions.Stats;
using _Game.Scripts.Player.Modules.Experience;
using _Game.Scripts.Player.Modules.Health;
using _Game.Scripts.Player.Modules.Movement;
using _Game.Scripts.Player.Modules.Vision;
using _Game.Scripts.World.Food;

namespace _Game.Scripts.Player
{
public class PlayerStats
{
    public float DamageReflection => _stats.GetValueOrDefault(EvolutionType.DamageReflection);
    public float EatingSpeed => _stats.GetValueOrDefault(EvolutionType.EatingSpeed);
    public float PhysicalDamage => _stats.GetValueOrDefault(EvolutionType.PhysicalDamage);
    public float Stamina => _stats.GetValueOrDefault(EvolutionType.Stamina);

    public event Action<FoodItem> OnFoodEaten;
    
    // Modules
    
    public VisionStats Vision { get; } = new();
    public MovementStats Movement { get; } = new();
    public ExperienceStats Experience { get; }  = new();
    public HealthStats Health { get; } = new();
    
    //Evolutions
    private readonly List<Evolution> _evolutions = new();
    public event Action<Evolution> OnEvolutionAdded;
    //Stats
    private readonly Dictionary<EvolutionType, float> _stats = new();

    public PlayerStats(PlayerConfig config)
    {
        AddStats(config.Stats);
        Experience.Initialize(config.ExperienceConfig);
        Health.Initialize(_stats.GetValueOrDefault(EvolutionType.MaxHealth));
    }

    public void Eat(int amount, FoodItem food)
    {
        Experience.AddExperience(amount);
        OnFoodEaten?.Invoke(food);

        food.Release();
    }

    public void AddEvolution(Evolution evolution)
    {
        _evolutions.Add(evolution);
        OnEvolutionAdded?.Invoke(evolution);
        
        AddStats(evolution.Stats);
    }

    public float TakeDamage(float damage)
    {
        Health.TakeDamage(damage);
        return ReturnDamage(damage);
    }

    private float ReturnDamage(float damage)
    {
        return damage * DamageReflection / 100;
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

            switch (stat.Type)
            {
                case EvolutionType.VisionRadius:
                    Vision.UpdateRadius(_stats[EvolutionType.VisionRadius]);
                    break;
                case EvolutionType.SensoricsRadius:
                    Vision.UpdateSensoricsRadius(_stats[EvolutionType.SensoricsRadius]);
                    break;
                case EvolutionType.MoveSpeed:
                    Movement.UpdateMoveSpeed(_stats[EvolutionType.MoveSpeed]);
                    break;
                case EvolutionType.Acceleration:
                    Movement.UpdateAcceleration(_stats[EvolutionType.Acceleration]);
                    break;
                case EvolutionType.Inertia:
                    Movement.UpdateInertia(_stats[EvolutionType.Inertia]);
                    break;
                case EvolutionType.MaxHealth:
                    Health.UpdateMaxHealth(_stats[EvolutionType.MaxHealth]);
                    break;
                case EvolutionType.RegenerationSpeed:
                    Health.UpdateRegeneration(_stats[EvolutionType.RegenerationSpeed]);
                    break;
            }
        }
    }
}
}