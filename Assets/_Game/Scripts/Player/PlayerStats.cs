using System;
using System.Collections.Generic;
using System.Linq;
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
    // Modules
    public VisionModule Vision { get; }
    public MovementModule Movement { get; }
    public ExperienceController Experience { get; }
    public HealthModule Health { get; }
    public EatModule EatModule { get; }
    public AttackModule Attack { get; }
    
    //Evolutions
    private readonly List<Evolution> _evolutions = new();
    public event Action<Evolution> OnEvolutionAdded;
    //Stats
    private readonly Dictionary<EvolutionType, float> _stats = new();
    public event Action<EvolutionType, float> OnStatUpdated;
    
    private PlayerConfig _config;
    private readonly Dictionary<EvolutionType, float> _basicStats = new();
    
    
    public PlayerStats(PlayerConfig config)
    {
        _config = config;
        
        Vision = new(this);
        Movement = new(this);
        Health = new(this);
        EatModule = new (this);
        Experience = new(config.ExperienceConfig, EatModule);
        Attack = new(this);
        AddInitialStats(_config.InitialConfig.Stats);
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

    private void AddInitialStats(List<Stat> stats)
    {
        foreach (var stat in stats)
        {
            _stats.Add(stat.Type, stat.Value);
            _basicStats.Add(stat.Type, stat.Value);
        }
    }

    private void AddStats(List<Stat> stats)
    {
        foreach (var stat in stats)
        {
            if (!HasStat(stat))
            {
                var basicStat = _config.BasicConfig.Stats
                    .First(t => t.Type == stat.Type);

                _stats.Add(stat.Type, basicStat.Value);
                _basicStats.Add(stat.Type, basicStat.Value);
            }
            _stats[stat.Type] *= 1 + stat.Value / 100f;

            UpdateStat(stat);
        }
    }

    private void UpdateStat(Stat stat)
    {
        OnStatUpdated?.Invoke(
            stat.Type,
            _stats[stat.Type]
        );
    }

    public void Dispose()
    {
        Vision.Dispose();
        Movement.Dispose();
        Experience.Dispose();
        Health.Dispose();
        EatModule.Dispose();
        Attack.Dispose();
    }
}
}