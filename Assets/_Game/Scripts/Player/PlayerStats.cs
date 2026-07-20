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

    //Level
    private readonly float _levelScaler;
    private float _levelSet;
    private float _experience;
    private int _level;
    
    public event Action<float> OnExperienceChanged;
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

    public void AddExperience(float amount)
    {
        _experience += amount;
        OnExperienceChanged?.Invoke(_experience);
        UpdateLevel();
    }
    
    private void UpdateLevel()
    {
        while (_experience >= _levelSet)
        {
            AddExperience(-_levelSet);
            _level++;
            OnLevelChanged?.Invoke(_level);
            _levelSet *= _levelScaler;
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