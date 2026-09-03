using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Experience;
using _Game.Scripts.GamePlay.UI.Bar;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities.Experience
{
public class ExperienceModule: IStartable, IDisposable, IResource
{
    public int Level { get; private set; }

    private int _levelSet;
    private int _experience;

    private readonly List<ExperienceService> _experienceServices = new();

    private int _levelScaler;
    
    [Inject] private EntityExperienceConfig _config;
    [Inject] private EntityModel _model;
    [Inject] private ExperienceFactory _expFactory;
    
    public event Action<int> OnLevelChanged;

    public event Action<float, float> OnValueChanged;

    public void Start()
    {
        if (_config.ExperienceConfig == null) return;
        
        _levelSet = _config.ExperienceConfig.LevelSet;
        _levelScaler = _config.LevelScaler;
        Level = _config.ExperienceConfig.Level;
        
        for (var i = 0; i < Level; i++)
        {
            _levelSet += _levelScaler;
            _levelScaler++;
        }
        
        if (_config.ExperienceConfig.ExperienceTypes.Count == 0) return;
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
        _experience += amount;
        OnValueChanged?.Invoke(_experience, _levelSet);
        UpdateLevel();
    }
    
    private void UpdateLevel()
    {
        while (_experience >= _levelSet)
        {
            UpdateExperience(-_levelSet);
            Level++;
            OnLevelChanged?.Invoke(Level);
            _levelSet += _levelScaler;
            OnValueChanged?.Invoke(_experience, _levelSet);
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