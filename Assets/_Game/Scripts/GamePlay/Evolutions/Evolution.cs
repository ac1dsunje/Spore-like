using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Experience;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Rarities;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Evolutions
{
public class Evolution: IDisposable, IStatSource
{
    public EvolutionConfig Config { get; private set; }
    public EvolutionState State { get; private set; }
    public string Name => _rarity ? $"{_rarity.Name} {Config.Name}" : $"{Config.Name}";
    public List<EvolutionStat> Stats { get; private set; } = new();
    public Sprite Frame => _rarity.Sprite;
    
    private RarityConfig _rarity;
    private EntityModel _entity;
    public event Action OnRarityChanged;
    
    //Level
    private readonly ExperienceFactory _expFactory = new();
    private readonly List<ExperienceService> _experienceServices = new();
    private int _experiencePoints;
    private int _levelSet;
    private int _level;
    public event Action<int> OnEvolutionExperienceChanged;
    public event Action<Evolution, int> OnLevelUp;

    public Evolution(EvolutionConfig config)
    {
        Config = config;
        SetStats();
        SetState(Config.State);
    }

    public List<SourceStat> GetStats()
    {
        return Stats.Select(stat => new SourceStat(stat.Type, stat.CurrentValue, stat.Operation, stat.Target)).ToList();
    }

    public void Apply(EntityModel entityModel)
    {
        _entity = entityModel;
        Activate();
        SubscribeExperienceServices();
    }

    private void SubscribeExperienceServices()
    {
        foreach (var config in Config.ExperienceConfig.ExperienceTypes)
        {
            var experienceType = _expFactory.GetService(config, _entity);
            _experienceServices.Add(experienceType);
            experienceType.OnExperienceGained += UpdateExperience;
        }
    }
    
    private void Activate() => SetState(EvolutionState.IsActive);

    public void Unlock() => SetState(EvolutionState.IsAble);

    public void Block() => SetState(EvolutionState.IsLocked);
    
    public void SetRarity(RarityConfig rarity)
    {
        UseRarity(rarity);

        SetInitialLevel(_rarity.Index);
    }

    public void UpdateRarity(RarityConfig rarity)
    {
        UseRarity(rarity);
        
        OnRarityChanged?.Invoke();
        _entity.Stats.UpdateSource(this);
    }

    private void UseRarity(RarityConfig rarity)
    {
        _rarity = rarity;
        foreach (var stat in Stats)
        {
            stat.UseRarity(_rarity.Scaler);
        }
    }

    private void SetInitialLevel(int value)
    {
        _level = value;
        _levelSet = Config.ExperienceConfig.LevelSet + (int)(Config.ExperienceConfig.LevelSet / 2f * (Math.Pow(2, _level - 1) - 1));
    }

    private void SetStats()
    {
        Stats.Clear();
        foreach (var stat in Config.Stats)
        {
            var newStat = new EvolutionStat(stat);
            Stats.Add(newStat);
        }
    }

    private void UpdateExperience(int amount)
    {
        while (true)
        {
            _experiencePoints += amount;
            OnEvolutionExperienceChanged?.Invoke(_experiencePoints);

            if (_experiencePoints < _levelSet) return;
            UpdateLevel();
            amount = -_levelSet;
        }
    }

    private void UpdateLevel()
    {
        _level++;
        _levelSet += (int)(Config.ExperienceConfig.LevelSet / 2f * Math.Pow(2, _level - 2));
        OnLevelUp?.Invoke(this, _level);
    }

    private void SetState(EvolutionState state) => State = state;

    public void Dispose()
    {
        if (_experienceServices == null) return;
        foreach (var experienceService in _experienceServices)
        {
            experienceService.Dispose();
            experienceService.OnExperienceGained -= UpdateExperience;
        }
    }
}
}