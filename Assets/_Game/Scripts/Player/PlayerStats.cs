using System;
using System.Collections.Generic;
using _Game.Scripts.Evolutions;
using _Game.Scripts.Evolutions.Stats;
namespace _Game.Scripts.Player
{
public class PlayerStats
{
    public float MoveSpeed => _stats.GetValueOrDefault(EvolutionType.MoveSpeed);
    public float VisionRadius => _stats.GetValueOrDefault(EvolutionType.VisionRadius);
    public float SensoricsRadius => _stats.GetValueOrDefault(EvolutionType.SensoricsRadius);
    public float Acceleration => _stats.GetValueOrDefault(EvolutionType.Acceleration);
    public float DamageReflection => _stats.GetValueOrDefault(EvolutionType.DamageReflection);
    public float EatingSpeed => _stats.GetValueOrDefault(EvolutionType.EatingSpeed);
    public float PhysicalDamage => _stats.GetValueOrDefault(EvolutionType.PhysicalDamage);
    public float RegenerationSpeed => _stats.GetValueOrDefault(EvolutionType.RegenerationSpeed);
    public float Inertia => _stats.GetValueOrDefault(EvolutionType.Inertia);
    public float Stamina => _stats.GetValueOrDefault(EvolutionType.Stamina);

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
    private readonly Dictionary<EvolutionType, float> _stats = new();

    public PlayerStats(PlayerConfig config)
    {
        AddStats(config.Stats);
        
        _levelSet = config.ExperienceConfig.LevelSet;
        _levelScaler = config.ExperienceConfig.LevelScaler;
    }

    public void AddExperience(int amount)
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
        
        AddStats(evolution.Stats);
    }

    private void AddStats(List<Stat> stat)
    {
        foreach (var stats in stat)
        {
            if (!_stats.ContainsKey(stats.Type))
            {
                _stats.Add(stats.Type, stats.Value);
            }
            else
            {
                _stats[stats.Type] *= 1 + stats.Value/100;
            }
        }
    }
}
}