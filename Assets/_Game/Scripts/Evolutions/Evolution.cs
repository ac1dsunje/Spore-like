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
    public string Description { get; private set; }
    public Sprite Sprite { get; private set; }
    public Sprite Frame { get; private set; }
    
    public EvolutionConfig[] Unlocks { get; private set; }
    public EvolutionConfig[] Blocks { get; private set; }
    
    private RarityConfig _rarity;
    protected PlayerStats Player;
    
    //Level
    private int _experiencePoints;
    public event Action<int> OnEvolutionExperienceChanged;

    protected Evolution(EvolutionConfig config)
    {
        SetConfig(config);
    }

    private void SetConfig(EvolutionConfig config)
    {
        Config = config;
        Name = Config.Name;
        Description = Config.Description;
        Sprite = Config.Sprite;
        
        Unlocks = Config.Unlocks;
        Blocks = Config.Blocks;

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
        Frame = _rarity.Sprite;
    }

    public virtual void Apply(PlayerStats playerStats)
    {
        Player = playerStats;
        SetState(EvolutionState.IsActive);
    }

    protected void UpdateExperience(int amount)
    {
        _experiencePoints += amount;
        OnEvolutionExperienceChanged?.Invoke(_experiencePoints);
        Debug.Log($"{Name}`s experience: {_experiencePoints}");
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

    private void SetState(EvolutionState state) => State = state;

    public abstract void Dispose();
}
}