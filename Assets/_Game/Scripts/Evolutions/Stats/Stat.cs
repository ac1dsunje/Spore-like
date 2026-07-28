using System;
using UnityEngine;

namespace _Game.Scripts.Evolutions.Stats
{
[Serializable]
public class Stat
{
    [field: SerializeField] public StatType Type {get; private set;}
    [field: SerializeField] public float Value {get; private set;}
    [field: SerializeField] public bool UpdatesByRarity {get; private set;}
    
    public float CurrentValue {get; private set;}

    public Stat(Stat stat)
    {
        Type = stat.Type;
        Value = stat.Value;
        UpdatesByRarity = stat.UpdatesByRarity;
    }

    public void UseRarity(float scaler)
    {
        CurrentValue = UpdatesByRarity ? Value * scaler : Value;
    }
}
}