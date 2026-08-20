using System;
using UnityEngine;

namespace _Game.Scripts.GamePlay
{
public enum StatOperation
{
    Add,
    Multiply,
    Percent
}
public enum StatTarget
{
    Base,
    Total
}
[Serializable]
public class SourceStat : Stat
{
    [field: SerializeField] public StatOperation Operation { get; private set; }

    [field: SerializeField] public StatTarget Target { get; private set; }
    
    public virtual float CurrentValue => Value;
    
    public SourceStat(StatType type, float value, StatOperation operation, StatTarget target) : base(type, value)
    {
        Operation = operation;
        Target = target;
    }

    public string GetDescription()
    {
        return Operation switch
        {
            StatOperation.Add => $"{Type}{(CurrentValue > 0 ? " +" : " ")}{CurrentValue}",
            StatOperation.Multiply => $"{Type} *{CurrentValue}",
            StatOperation.Percent => $"{Type} {CurrentValue}%",
            _ => $"{Type} {CurrentValue}"
        };
    }
}
}