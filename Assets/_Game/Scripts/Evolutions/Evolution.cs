using System;
using System.Collections.Generic;
using _Game.Scripts.Evolutions.Stats;
using _Game.Scripts.Player;
using _Game.Scripts.Rarities;
using UnityEngine;

namespace _Game.Scripts.Evolutions
{
public abstract class Evolution: IDisposable
{
    public EvolutionConfig Config { get; private set; }
    public EvolutionState State { get; private set; }
    public string Name { get; private set; }
    public List<Stat> Stats { get; private set; } = new();
    public Sprite Frame { get; private set; }
    
    private RarityConfig _rarity;
    protected PlayerStats Player;
    public event Action OnRarityChanged;
    
    //Level
    private int _experiencePoints;
    private int _levelSet;
    private int _level;
    public event Action<int> OnEvolutionExperienceChanged;
    public event Action<Evolution, int> OnLevelUp;

    protected Evolution(EvolutionConfig config)
    {
        SetConfig(config);
    }

    private void SetConfig(EvolutionConfig config)
    {
        Config = config;

        SetStats();
        
        SetState(Config.State);
    }
    
    public void SetRarity(RarityConfig rarity)
    {
        _rarity = rarity;
        Name = $"{_rarity.Name} {Config.Name}";
        foreach (var stat in Stats)
        {
            stat.UseRarity(_rarity.Scaler);
        }

        _level = _rarity.Index;
        _levelSet = Config.ExperienceForFirstLevel + (int)(Config.ExperienceForFirstLevel / 2f * (Math.Pow(2, _level - 1) - 1));
        
        Frame = _rarity.Sprite;
    }

    public void UpdateRarity(RarityConfig rarity)
    {
        _rarity = rarity;
        Name = $"{_rarity.Name} {Config.Name}";
        foreach (var stat in Stats)
        {
            stat.UseRarity(_rarity.Scaler);
        }
        Frame = _rarity.Sprite;
        OnRarityChanged?.Invoke();
        Player.UpdateEvolution(this);
    }

    public void SetPlayer(PlayerStats playerStats)
    {
        Player = playerStats;
    }

    public virtual void Apply()
    {
        SetState(EvolutionState.IsActive);
    }

    public void Unlock() => SetState(EvolutionState.IsAble);

    public void Block() => SetState(EvolutionState.IsLocked);

    private void SetStats()
    {
        Stats.Clear();
        foreach (var stat in Config.Stats)
        {
            var newStat = new Stat(stat);
            Stats.Add(newStat);
        }
    }

    protected void UpdateExperience(int amount)
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

    public abstract void Dispose();
}
}