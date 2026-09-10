using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Experience;
using _Game.Scripts.GamePlay.Interfaces;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities.Experience
{
public class ExperienceModule : IStartable, IDisposable, IStatWithLimit
{
    public int Level { get; private set; }

    private int _levelSet;
    private int _experience;

    private readonly List<ExperienceService> _experienceServices = new();

    private int _levelScaler;
    
    private readonly EntityExperienceConfig _config;
    private readonly EntityScope _entity;
    private readonly ExperienceFactory _expFactory;
    
    public event Action<int> OnLevelChanged;

    public event Action<float, float> OnValueChanged;

    public ExperienceModule(EntityExperienceConfig config, EntityScope entity, ExperienceFactory factory)
    {
        _config = config;
        _entity = entity;
        _expFactory = factory;
    }

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
        SubscribeExperienceServices(_config.ExperienceConfig);
    }
    
    private void SubscribeExperienceServices(ExperienceConfig config)
    {
        foreach (var type in config.ExperienceTypes)
        {
            var experienceType = _expFactory.GetService(type, _entity);
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