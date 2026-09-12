using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Experience;
using R3;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities.Experience
{
public class ExperienceModule : IStartable, IDisposable
{
    public int Level { get; private set; }
    
    public ReadOnlyReactiveProperty<float> Current => _current;
    public ReadOnlyReactiveProperty<float> Max => _max;

    private readonly ReactiveProperty<float> _max = new();
    private readonly ReactiveProperty<float> _current = new();

    private readonly List<ExperienceService> _experienceServices = new();

    private int _levelScaler;
    
    private readonly ExperienceConfig _config;
    private readonly EntityScope _entity;
    private readonly ExperienceFactory _expFactory;
    
    public event Action<int> OnLevelChanged;

    public ExperienceModule(ExperienceConfig config, EntityScope entity, ExperienceFactory factory)
    {
        _config = config;
        _entity = entity;
        _expFactory = factory;
    }

    public void Start()
    {
        if (_config == null) return;
        
        _max.Value = _config.LevelSet;
        _levelScaler = 1;
        Level = _config.Level;
        
        for (var i = 0; i < Level; i++)
        {
            _max.Value += _levelScaler;
            _levelScaler++;
        }
        
        if (_config.ExperienceTypes.Count == 0) return;
        SubscribeExperienceServices(_config);
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

    private void UpdateExperience(float amount)
    {
        _current.Value += amount;
        UpdateLevel();
    }
    
    private void UpdateLevel()
    {
        while (_current.Value >= _max.Value)
        {
            UpdateExperience(-_max.Value);
            Level++;
            OnLevelChanged?.Invoke(Level);
            _max.Value += _levelScaler;
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