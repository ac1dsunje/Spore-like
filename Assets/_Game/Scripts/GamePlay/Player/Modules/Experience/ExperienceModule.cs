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
    
    private PlayerExperienceConfig _config;
    private EntityModel _model;
    
    public event Action<int> OnLevelChanged;

    public event Action<float, float> OnValueChanged;
    
    [Inject]
    public ExperienceModule(PlayerExperienceConfig config, EntityModel model)
    {
        LevelSet = config.ExperienceConfig.LevelSet;
        _levelScaler = config.LevelScaler;
        _config = config;
        _model = model;
    }

    public void Initialize()
    {
        SubscribeExperienceServices(_config.ExperienceConfig, _model);
    }
    
    private void SubscribeExperienceServices(ExperienceConfig config, EntityModel model)
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