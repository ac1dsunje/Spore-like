using System;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Evolutions
{
[Serializable]
public class EvolutionStat
{
    [field: SerializeField] public Stat Stat { get; private set; }
    [field: SerializeField] public bool UpdatesByRarity {get; private set;}
    
    public StatType Type => Stat.Type;
    public float Value => Stat.Value;
    
    public float CurrentValue {get; private set;}

    public EvolutionStat(EvolutionStat config)
    {
        Stat = config.Stat;
        UpdatesByRarity = config.UpdatesByRarity;
    }

    public void UseRarity(float scaler)
    {
        CurrentValue = UpdatesByRarity ? Stat.Value * scaler : Stat.Value;
    }
}
}