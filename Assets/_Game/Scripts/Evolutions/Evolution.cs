using System.Collections.Generic;
using _Game.Scripts.Evolutions.Stats;
using UnityEngine;

namespace _Game.Scripts.Evolutions
{
public enum EvolutionState
{
    IsHidden,
    IsAble,
    IsActive,
    IsLocked
}

public abstract class Evolution
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
    
    private EvolutionRarityConfig _rarity;

    public void SetConfig(EvolutionConfig config)
    {
        Config = config;
        Name = Config.Name;
        Description = Config.Description;
        Sprite = Config.Sprite;
        
        Unlocks = Config.Unlocks;
        Blocks = Config.Blocks;

        foreach (var stat in Config.Stats)
        {
            Stats.Add(stat);
        }
        
        SetState(Config.State);
    }
    
    public void SetRarity(EvolutionRarityConfig rarity)
    {
        _rarity = rarity;
        for (var i = 0; i < Stats.Count; i++)
        {
            Stats[i].Value = Config.Stats[i].Value * _rarity.Scaler;
        }
        Frame = _rarity.Sprite;
    }

    public virtual void Apply()
    {
        Debug.Log($"Applying{_rarity.Name} {Name}");
        SetState(EvolutionState.IsActive);
    }

    public void Unlock()
    {
        Debug.Log($"{Name} is unlocked");
        SetState(EvolutionState.IsAble);
    }

    public void Block()
    {
        Debug.Log($"{Name} is blocked");
        SetState(EvolutionState.IsLocked);
    }

    private void SetState(EvolutionState state) => State = state;
}
}