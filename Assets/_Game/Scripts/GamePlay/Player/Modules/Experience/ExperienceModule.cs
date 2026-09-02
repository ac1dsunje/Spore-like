using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Experience;
using _Game.Scripts.GamePlay.UI.Bar;

namespace _Game.Scripts.GamePlay.Player.Modules.Experience
{
public class ExperienceModule: IDisposable, IResource
{
    private int _levelSet;
    private int _experience;
    
    private readonly ExperienceFactory _expFactory = new();
    private readonly List<ExperienceService> _experienceServices = new();
    
    private int _level;
    private int _levelScaler;
    
    private EntityExperienceConfig _config;
    private EntityModel _model;
    
    public event Action<int> OnLevelChanged;

    public event Action<float, float> OnValueChanged;

    public void Initialize(EntityModel model, EntityExperienceConfig config)
    {
        _model = model;
        _config = config;
        
        if (_config.ExperienceConfig == null) return;
        
        _levelSet = _config.ExperienceConfig.LevelSet;
        _levelScaler = _config.LevelScaler;
        if (_config.ExperienceConfig == null || _config.ExperienceConfig.ExperienceTypes.Count == 0) return;
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
            _level++;
            OnLevelChanged?.Invoke(_level);
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