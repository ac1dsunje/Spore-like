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
    
    //Level
    private int _experiencePoints;
    private int _level;
    private int _levelSet = 5;
    public event Action<int> OnEvolutionExperienceChanged;
    public event Action<int> OnLevelChanged;

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
        for (var i = 0; i < Stats.Count; i++)
        {
            Stats[i].SetValue(Config.Stats[i].Value * _rarity.Scaler);
        }
        _level = _rarity.Index;
        Frame = _rarity.Sprite;
    }

    public virtual void Apply(PlayerStats playerStats)
    {
        Player = playerStats;
        SetState(EvolutionState.IsActive);
    }

    public void Unlock() => SetState(EvolutionState.IsAble);

    public void Block() => SetState(EvolutionState.IsLocked);

    private void SetStats()
    {
        Stats.Clear();
        foreach (var stat in Config.Stats)
        {
            var newStat = new Stat(stat.Type, stat.Value);
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
        _levelSet++;
    }

    private void UpdateLevel()
    {
        _level++;
        Debug.Log($"{Name}`s level {_level}");
        OnLevelChanged?.Invoke(_level);
        
        // ToDo: set next rarity
    }

    private void SetState(EvolutionState state) => State = state;

    public abstract void Dispose();
}
}