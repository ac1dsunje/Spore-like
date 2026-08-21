using System;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Types
{
[Serializable]
public class StatTypeData
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public StatType Type { get; private set; }
    [field: SerializeField, Range(-999999, 999999)] public float MinimalValue { get; private set; } = -999999;
    [field: SerializeField, Range(-999999, 999999)] public float MaximalValue { get; private set; } = 999999;

    public StatTypeData(StatType type)
    {
        Type = type;
        Name = Enum.GetName(typeof(StatType), type);
    }
}
}