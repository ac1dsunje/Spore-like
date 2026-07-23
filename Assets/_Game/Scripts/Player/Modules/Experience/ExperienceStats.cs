using System;
using _Game.Scripts.Player.Modules.Mouth;

namespace _Game.Scripts.Player.Modules.Experience
{
public class ExperienceStats: IDisposable
{
    private int _levelSet;
    private int _experience;
    private int _level;
    private int _levelScaler;
    
    public event Action<int> OnExperienceChanged;
    public event Action<int> OnExperienceGained;
    public event Action<int> OnLevelChanged;
    
    private EatStats _eatStats;
    
    public void Initialize(ExperienceConfig config, EatStats eatStats)
    {
        _levelSet = config.LevelSet;
        _levelScaler = config.LevelScaler;
        
        _eatStats = eatStats;
        _eatStats.OnFoodPointsAchieved += AddExperience;
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

    public void Dispose()
    {
        _eatStats.OnFoodPointsAchieved -= AddExperience;
    }
}
}