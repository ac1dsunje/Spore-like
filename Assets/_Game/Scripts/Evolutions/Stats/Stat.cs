using System;
using UnityEngine;

namespace _Game.Scripts.Evolutions.Stats
{
[Serializable]
public class Stat
{
    [field: SerializeField] public EvolutionType Type {get; private set;}
    [field: SerializeField] public float Value {get; private set;}
    
    public float CurrentValue {get; private set;}

    public Stat(Stat stat)
    {
        Type = stat.Type;
        Value = stat.Value;
    }

    public void UseRarity(float scaler)
    {
        CurrentValue = Value * scaler;
    }
}
}