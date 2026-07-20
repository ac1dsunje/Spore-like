using System;
using _Game.Scripts.Player.Experience;
using _Game.Scripts.Player.Movement;

namespace _Game.Scripts.Player
{
public class PlayerStats
{
    public MovementConfig Movement { get; private set; }
    private readonly ExperienceConfig _experienceConfig;
    
    private float _experience;
    private float _levelSet;
    private int _level;
    
    public event Action<float> OnExperienceChanged;
    public event Action<int> OnLevelChanged;

    public PlayerStats(PlayerConfig config)
    {
        Movement = config.MovementConfig;
        _experienceConfig = config.ExperienceConfig;
        _levelSet = _experienceConfig._levelSet;
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
            _levelSet *= _experienceConfig._levelScaler;
        }
    }
}
}