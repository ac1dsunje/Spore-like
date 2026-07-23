using System;
using System.Collections.Generic;
using _Game.Scripts.Evolutions;
using _Game.Scripts.Evolutions.Stats;
using _Game.Scripts.Player.Modules;
using _Game.Scripts.Player.Modules.Attack;
using _Game.Scripts.Player.Modules.Experience;
using _Game.Scripts.Player.Modules.Health;
using _Game.Scripts.Player.Modules.Mouth;
using _Game.Scripts.Player.Modules.Movement;
using _Game.Scripts.Player.Modules.Vision;
using UnityEngine;

namespace _Game.Scripts.Player
{
public class PlayerStats: IDisposable
{
    // Modules
    public VisionModule Vision { get; private set; }
    public MovementModule Movement { get; private set; }
    public HealthModule Health { get; private set; }
    public EatModule EatModule { get; private set; }
    public AttackModule Attack { get; private set; }
    private List<StatModule> _modules = new();
    public ExperienceController Experience { get; }
    //Evolutions
    private readonly List<Evolution> _evolutions = new();
    public event Action<Evolution> OnEvolutionAdded;

    //Stats
    private readonly Dictionary<EvolutionType, float> _stats = new();
    private readonly Dictionary<EvolutionType, float> _basicStats = new();
    private readonly Dictionary<Evolution, Dictionary<EvolutionType, float>> _evolutionStats = new();

    public event Action<EvolutionType, float> OnStatUpdated;
    
    private PlayerConfig _config;
    
    
    public PlayerStats(PlayerConfig config)
    {
        _config = config;
        
        AddModules();
        Experience = new(config.ExperienceConfig, EatModule);
        
        AddInitialStats(_config.InitialConfig.Stats);
    }

    private void AddModules()
    {
        Vision = new(this);
        Movement = new(this);
        Health = new(this);
        EatModule = new (this);
        Attack = new(this);

        _modules.Add(Vision);
        _modules.Add(Movement);
        _modules.Add(Health);
        _modules.Add(EatModule);
        _modules.Add(Attack);
    }

    public void AddEvolution(Evolution evolution)
    {
        _evolutions.Add(evolution);
        OnEvolutionAdded?.Invoke(evolution);
        
        AddEvolutionStats(evolution);

        foreach (var stat in evolution.Stats)
        {
            RecalculateStat(stat.Type);
        }
    }

    public void UpdateEvolution(Evolution evolution)
    {
        var changedStats = new HashSet<EvolutionType>();

        foreach (var stat in evolution.Stats)
        {
            if (!_evolutionStats[evolution].ContainsKey(stat.Type) ||
                !Mathf.Approximately(_evolutionStats[evolution][stat.Type], stat.CurrentValue))
            {
                changedStats.Add(stat.Type);
            }
            
            _evolutionStats[evolution][stat.Type] = stat.CurrentValue;
        }

        foreach (var statType in changedStats)
        {
            RecalculateStat(statType);
        }
    }

    private void AddInitialStats(List<Stat> stats)
    {
        foreach (var stat in stats)
        {
            _basicStats.Add(stat.Type, stat.Value);
            _stats.Add(stat.Type, stat.Value);

            UpdateStat(stat.Type);
        }
    }

    private void AddEvolutionStats(Evolution evolution)
    {
        if (_evolutionStats.ContainsKey(evolution))
            return;

        var stats = new Dictionary<EvolutionType, float>();

        foreach (var stat in evolution.Stats)
        {
            stats.Add(stat.Type, stat.CurrentValue);
        }

        _evolutionStats.Add(evolution, stats);
    }

    private void RecalculateStat(EvolutionType type)
    {
        var value = _basicStats.GetValueOrDefault(type, 0f);

        foreach (var evolution in _evolutionStats)
        {
            if (evolution.Value.TryGetValue(type, out var statValue))
            {
                value += statValue;
            }
        }

        _stats[type] = value;

        UpdateStat(type);
    }

    private void UpdateStat(EvolutionType type) => OnStatUpdated?.Invoke(type, _stats[type]);

    public void Dispose()
    {
        foreach (var module in _modules)
        {
            module.Dispose();
        }
        Experience.Dispose();
    }
}
}