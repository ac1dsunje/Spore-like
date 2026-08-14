using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Experience;
using _Game.Scripts.GamePlay.UI.Bar;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Modules.Experience
{
public class ExperienceModule: IDisposable, IResource
{
    public int LevelSet { get; private set; }
    public int Experience { get; private set; }
    
    private readonly ExperienceFactory _expFactory = new();
    private readonly List<ExperienceService> _experienceServices = new();
    
    private int _level;
    private int _levelScaler;
    
    private ExperienceConfig _config;
    
    public event Action<int> OnLevelChanged;

    public event Action<float, float> OnValueChanged;
    
    [Inject]
    public ExperienceModule(ExperienceConfig config)
    {
        LevelSet = config.LevelSet;
        _levelScaler = config.LevelScaler;
        _config = config;
    }

    public void Initialize(PlayerModel playerModel)
    {
        SubscribeExperienceServices(_config, playerModel);
    }
    
    private void SubscribeExperienceServices(ExperienceConfig config, PlayerModel model)
    {
        foreach (var type in config.ExperienceTypes)
        {
            var experienceType = _expFactory.GetService(type, model);
            _experienceServices.Add(experienceType);
            experienceType.OnExperienceGained += UpdateExperience;
        }
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
        foreach (var experienceService in _experienceServices)
        {
            experienceService.Dispose();
            experienceService.OnExperienceGained -= UpdateExperience;
        }
    }
}
}