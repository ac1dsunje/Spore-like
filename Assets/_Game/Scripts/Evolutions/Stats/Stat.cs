using System;
using UnityEngine;

namespace _Game.Scripts.Evolutions.Stats
{
public enum EvolutionType
{
    MoveSpeed,
    VisionRadius,
    SensoricsRadius

}

[Serializable]
public class Stat
{
    [field: SerializeField] public EvolutionType Type { get; private set; }
    public float Value;
}
}