using System;
using System.Collections.Generic;
using _Game.Scripts.Evolutions.Experience;
using _Game.Scripts.Evolutions.Experience.Types;
using _Game.Scripts.Evolutions.Stats;
using _Game.Scripts.Player;
using _Game.Scripts.Rarities;
using UnityEngine;

namespace _Game.Scripts.Evolutions
{
public class Evolution: IDisposable
{
    public EvolutionConfig Config { get; private set; }
    public EvolutionState State { get; private set; }
    public string Name => _rarity ? $"{_rarity.Name} {Config.Name}" : $"{Config.Name}";
    public List<Stat> Stats { get; private set; } = new();
    public Sprite Frame => _rarity.Sprite;
    
    private RarityConfig _rarity;
    private PlayerStats _player;
    public event Action OnRarityChanged;
    
    //Level
    private readonly EvolutionExperienceFactory _expFactory = new();
    private IEvolutionExperience _experienceManager;
    private int _experiencePoints;
    private int _levelSet;
    private int _level;
    public event Action<int> OnEvolutionExperienceChanged;
    public event Action<Evolution, int> OnLevelUp;

    public Evolution(EvolutionConfig config)
    {
        SetConfig(config);
    }

    public void SetPlayer(PlayerStats playerStats) => _player = playerStats;

    public void Apply()
    {
        SetState(EvolutionState.IsActive);

        _experienceManager = _expFactory.GetMethod(Config.ExperienceType, _player);

        _experienceManager.OnExperienceGained += UpdateExperience;
    }

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
        _player.UpdateEvolution(this);
    }

    private void SetConfig(EvolutionConfig config)
    {
        Config = config;

        SetStats();
        
        SetState(Config.State);
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
        _levelSet = Config.ExperienceForFirstLevel + (int)(Config.ExperienceForFirstLevel / 2f * (Math.Pow(2, _level - 1) - 1));
    }

    private void SetStats()
    {
        Stats.Clear();
        foreach (var stat in Config.Stats)
        {
            var newStat = new Stat(stat);
            Stats.Add(newStat);
        }
    }

    private void UpdateExperience(int amount)
    {
        _experiencePoints += amount;
        OnEvolutionExperienceChanged?.Invoke(_experiencePoints);
        
        if (_experiencePoints >= _levelSet)
        {
            UpdateLevel();
            UpdateExperience(-_levelSet);
        }
    }

    private void UpdateLevel()
    {
        _level++;
        _levelSet += (int)(Config.ExperienceForFirstLevel / 2f * Math.Pow(2, _level - 2));
        OnLevelUp?.Invoke(this, _level);
    }

    private void SetState(EvolutionState state) => State = state;

    public void Dispose()
    {
        if (_experienceManager == null) return;
        _experienceManager.Dispose();
        _experienceManager.OnExperienceGained -= UpdateExperience;
    }
}
}