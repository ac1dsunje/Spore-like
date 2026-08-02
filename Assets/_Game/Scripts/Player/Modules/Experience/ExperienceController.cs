using System;
using _Game.Scripts.Player.Modules.Mouth;

namespace _Game.Scripts.Player.Modules.Experience
{
public class ExperienceController: IDisposable
{
    public int LevelSet { get; private set; }
    public int Experience { get; private set; }
    private int _level;
    private int _levelScaler;
    
    public event Action<int> OnExperienceChanged;
    public event Action<int> OnExperienceGained;
    public event Action<int> OnLevelChanged;
    public event Action<int> OnLevelSetChanged;
    
    private EatModule _eatModule;
    
    public ExperienceController(ExperienceConfig config, EatModule eatModule)
    {
        LevelSet = config.LevelSet;
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
        Experience += amount;
        OnExperienceChanged?.Invoke(Experience);
        UpdateLevel();
    }
    
    private void UpdateLevel()
    {
        while (Experience >= LevelSet)
        {
            UpdateExperience(-LevelSet);
            _level++;
            OnLevelChanged?.Invoke(_level);
            LevelSet += _levelScaler;
            OnLevelSetChanged?.Invoke(LevelSet);
            _levelScaler++;
        }
    }

    public void Dispose()
    {
        _eatModule.OnFoodPointsAchieved -= AddExperience;
    }
}
}