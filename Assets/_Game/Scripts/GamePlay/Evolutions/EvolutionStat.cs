using System;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Evolutions
{
[Serializable]
public class EvolutionStat: Stat
{
    [field: SerializeField] public bool UpdatesByRarity {get; private set;}
    
    public float CurrentValue {get; private set;}

    public EvolutionStat(EvolutionStat config): base (config.Type, config.Value)
    {
        UpdatesByRarity = config.UpdatesByRarity;
    }

    public void UseRarity(float scaler)
    {
        CurrentValue = UpdatesByRarity ? Value * scaler : Value;
    }
}
}