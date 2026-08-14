using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.GamePlay.Evolutions.Experience;
using _Game.Scripts.GamePlay.Player;
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
    private PlayerModel _player;

    public int Chance { get; private set; }
    public event Action OnRarityChanged;
    
    //Level
    private readonly EvolutionExperienceFactory _expFactory = new();
    private readonly List<EvolutionExperienceService> _experienceManagers = new();
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

    public void Initialize(PlayerModel playerModel, int chance)
    {
        _player = playerModel;
        Chance = chance;
    }

    public List<Stat> GetStats()
    {
        return Stats.Select(stat => new Stat(stat.Type, stat.CurrentValue)).ToList();
    }

    public void IncreaseChance(int amount) => Chance += amount;

    public void Apply()
    {
        Activate();
        SubscribeExperienceManagers();
    }

    private void SubscribeExperienceManagers()
    {
        foreach (var config in Config.ExperienceTypes)
        {
            var experienceType = _expFactory.GetMethod(config, _player);
            _experienceManagers.Add(experienceType);
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
        _player.Stats.UpdateSource(this);
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
        _levelSet += (int)(Config.ExperienceForFirstLevel / 2f * Math.Pow(2, _level - 2));
        OnLevelUp?.Invoke(this, _level);
    }

    private void SetState(EvolutionState state) => State = state;

    public void Dispose()
    {
        if (_experienceManagers == null) return;
        foreach (var experienceManager in _experienceManagers)
        {
            experienceManager.Dispose();
            experienceManager.OnExperienceGained -= UpdateExperience;
        }
    }
}
}