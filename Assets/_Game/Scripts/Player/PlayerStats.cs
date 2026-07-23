using System;
using System.Collections.Generic;
using _Game.Scripts.Evolutions;
using _Game.Scripts.Evolutions.Stats;
using _Game.Scripts.Player.Modules.Attack;
using _Game.Scripts.Player.Modules.Experience;
using _Game.Scripts.Player.Modules.Health;
using _Game.Scripts.Player.Modules.Mouth;
using _Game.Scripts.Player.Modules.Movement;
using _Game.Scripts.Player.Modules.Vision;

namespace _Game.Scripts.Player
{
public class PlayerStats: IDisposable
{
    public float Stamina => _stats.GetValueOrDefault(EvolutionType.Stamina);
    
    // Modules
    
    public VisionStats Vision { get; } = new();
    public MovementStats Movement { get; } = new();
    public ExperienceStats Experience { get; }  = new();
    public HealthStats Health { get; } = new();
    public EatStats EatStats { get; } = new();
    public AttackStats Attack { get; } = new();
    
    //Evolutions
    private readonly List<Evolution> _evolutions = new();
    public event Action<Evolution> OnEvolutionAdded;
    //Stats
    private readonly Dictionary<EvolutionType, float> _stats = new();

    public PlayerStats(PlayerConfig config)
    {
        AddStats(config.Stats);
        Experience.Initialize(config.ExperienceConfig, EatStats);
        Health.Initialize(_stats.GetValueOrDefault(EvolutionType.MaxHealth));
    }

    public bool HasStat(Stat stat)
    {
        return _stats.ContainsKey(stat.Type);
    }
    
    public void AddEvolution(Evolution evolution)
    {
        _evolutions.Add(evolution);
        OnEvolutionAdded?.Invoke(evolution);
        
        AddStats(evolution.Stats);
    }

    private void AddStats(List<Stat> stats)
    {
        foreach (var stat in stats)
        {
            if (!_stats.ContainsKey(stat.Type))
            {
                _stats.Add(stat.Type, stat.BasicValue);
            }
            else
            {
                _stats[stat.Type] *= 1 + stat.Value / 100f;
            }

            UpdateStats(stat);
        }
    }

    private void UpdateStats(Stat stat)
    {
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
            case EvolutionType.EatingSpeed:
                EatStats.UpdateEatingSpeed(_stats[EvolutionType.EatingSpeed]);
                break;
            case EvolutionType.DamageReflection:
                Attack.UpdateDamageReflection(_stats[EvolutionType.DamageReflection]);
                break;
            case EvolutionType.PhysicalDamage:
                Attack.UpdatePhysicalDamage(_stats[EvolutionType.PhysicalDamage]);
                break;
        }
    }

    public void Dispose()
    {
        Experience.Dispose();
    }
}
}