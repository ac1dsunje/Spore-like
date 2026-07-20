using System;
using System.Collections.Generic;
using _Game.Scripts.Evolutions;

namespace _Game.Scripts.Player
{
public class PlayerStats
{
    // Movement
    public float MoveSpeed { get; private set; }
    
    //Level
    private readonly float _levelScaler;
    private float _levelSet;
    private float _experience;
    private int _level;
    
    public event Action<float> OnExperienceChanged;
    public event Action<int> OnLevelChanged;
    
    //Evolutions
    private readonly List<Evolution> _evolutions = new();

    public PlayerStats(PlayerConfig config)
    {
        MoveSpeed = config.MovementConfig.MoveSpeed;
        
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
    }
}
}