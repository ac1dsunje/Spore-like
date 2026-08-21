using System;
using _Game.Scripts.GamePlay.Types;
using UnityEngine;

namespace _Game.Scripts.GamePlay
{
[Serializable]
public class Stat
{
    [field: SerializeField] public StatType Type {get; private set;}
    [field: SerializeField] public float Value {get; private set;}

    public Stat(StatType type, float value)
    {
        Type = type;
        Value = value;
    }
}
}