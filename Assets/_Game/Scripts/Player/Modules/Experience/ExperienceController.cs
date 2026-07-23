using System;
using _Game.Scripts.Player.Modules.Mouth;

namespace _Game.Scripts.Player.Modules.Experience
{
public class ExperienceController: IDisposable
{
    private int _levelSet;
    private int _experience;
    private int _level;
    private int _levelScaler;
    
    public event Action<int> OnExperienceChanged;
    public event Action<int> OnExperienceGained;
    public event Action<int> OnLevelChanged;
    public event Action<int> OnLevelSetChanged;
    
    private EatModule _eatModule;
    
    public ExperienceController(ExperienceConfig config, EatModule eatModule)
    {
        _levelSet = config.LevelSet;
        _levelScaler = config.LevelScaler;
        
        _eatModule = eatModule;
        _eatModule.OnFoodPointsAchieved += AddExperience;
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
            OnLevelSetChanged?.Invoke(_levelSet);
            _levelScaler++;
        }
    }

    public void Dispose()
    {
        _eatModule.OnFoodPointsAchieved -= AddExperience;
    }
}
}