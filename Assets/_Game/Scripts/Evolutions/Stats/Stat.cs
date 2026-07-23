using System;
using UnityEngine;

namespace _Game.Scripts.Evolutions.Stats
{
[Serializable]
public class Stat
{
    [field: SerializeField] public EvolutionType Type {get; private set;}
    [field: SerializeField] public float Value {get; private set;}
    [field: SerializeField] public float BasicValue {get; private set;}

    public bool UsePercentValue {get; private set;}

    public Stat(Stat stat)
    {
        Type = stat.Type;
        Value = stat.Value;
        BasicValue = stat.BasicValue;
    }

    public void UseRarity(float scaler)
    {
        Value *= scaler;
        BasicValue *= scaler;
    }

    public void SetPercentValue(bool set)
    {
        UsePercentValue = set;
    }
}
}