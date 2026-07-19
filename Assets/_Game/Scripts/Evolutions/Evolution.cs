using _Game.Scripts.Player;
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
    private EvolutionRarityConfig _rarity;
    private PlayerController _player;
    
    public EvolutionState State { get; private set; }
    
    public string Name { get; private set; }
    public float Value { get; private set; }
    public string Description { get; private set; }
    public Sprite Sprite { get; private set; }
    public Sprite Frame { get; private set; }
    
    public EvolutionConfig[] Unlocks { get; private set; }
    public EvolutionConfig[] Blocks { get; private set; }

    public void SetConfig(EvolutionConfig config)
    {
        Config = config;
        ParseConfig();
    }
    
    public void SetPlayer(PlayerController player) => _player = player;
    
    public void SetRarity(EvolutionRarityConfig rarity)
    {
        _rarity = rarity;
        
        Name = $"{_rarity.Name} {Config.Name}";
        Value = Config.BasicValue * _rarity.Scaler;
        Frame = _rarity.Sprite;
    }

    public virtual void Apply()
    {
        Debug.Log($"Applying {Name} with Rarity {_rarity.Name}");
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

    private void ParseConfig()
    {
        Name = $"{Config.Name}";
        Description = Config.Description;
        Sprite = Config.Sprite;
        
        Unlocks = Config.Unlocks;
        Blocks = Config.Blocks;
        
        SetState(Config.State);
    }
}
}