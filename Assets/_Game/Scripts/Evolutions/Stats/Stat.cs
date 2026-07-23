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

    public Stat(EvolutionType type, float value)
    {
        Type = type;
        Value = value;
    }

    public void SetValue(float value)
    {
        Value = value;
    }
}
}