using System;
using _Game.Scripts.GamePlay.Entity;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Evolutions
{
[Serializable]
public class EvolutionStat : SourceStat
{
    [field: SerializeField] public bool UpdatesByRarity { get; private set; }
    
    public float CurrentRarityValue { get; private set; }

    public override float CurrentValue => CurrentRarityValue;

    public EvolutionStat(EvolutionStat config) : base(config.Type, config.Value, config.Operation, config.Target)
    {
        UpdatesByRarity = config.UpdatesByRarity;
        CurrentRarityValue = Value;
    }
    
    public void UseRarity(float multiplier)
    {
        CurrentRarityValue = UpdatesByRarity ? Value * multiplier : Value;
    }
}
}