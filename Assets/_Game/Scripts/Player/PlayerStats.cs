using System;

namespace _Game.Scripts.Player
{
public class PlayerStats
{
    public float MoveSpeed { get; private set; }
    
    private readonly float _levelScaler;
    private float _levelSet;
    private float _experience;
    private int _level;
    
    public event Action<float> OnExperienceChanged;
    public event Action<int> OnLevelChanged;

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
}
}