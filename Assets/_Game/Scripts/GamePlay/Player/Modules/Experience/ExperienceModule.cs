using System;
using _Game.Scripts.GamePlay.Module;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Modules.Experience
{
public class ExperienceModule: IDisposable, IResource
{
    public int LevelSet { get; private set; }
    public int Experience { get; private set; }
    private int _level;
    private int _levelScaler;
    
    public event Action<int> OnExperienceGained;
    public event Action<int> OnLevelChanged;

    public event Action<float, float> OnValueChanged;
    
    private MouthModule _mouthModule;
    
    [Inject]
    public ExperienceModule(ExperienceConfig config, MouthModule mouthModule)
    {
        LevelSet = config.LevelSet;
        _levelScaler = config.LevelScaler;
        
        _mouthModule = mouthModule;
        _mouthModule.OnFoodPointsAchieved += AddExperience;
    }

    private void AddExperience(float amount)
    {
        OnExperienceGained?.Invoke((int)amount);
        UpdateExperience((int)amount);
    }

    private void UpdateExperience(int amount)
    {
        Experience += amount;
        OnValueChanged?.Invoke(Experience, LevelSet);
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
            OnValueChanged?.Invoke(Experience, LevelSet);
            _levelScaler++;
        }
    }

    public void Dispose()
    {
        _mouthModule.OnFoodPointsAchieved -= AddExperience;
    }
}
}