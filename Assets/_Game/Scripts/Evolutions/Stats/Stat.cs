using System;

namespace _Game.Scripts.Evolutions.Stats
{
public enum EvolutionType
{
    MoveSpeed,
    VisionRadius,
    SensoricsRadius,
    Acceleration,
    DamageReflection,
    EatingSpeed,
    PhysicalDamage,
    RegenerationSpeed,
    Inertia,
    Stamina,
}

[Serializable]
public class Stat
{
    public EvolutionType Type;
    public float Value;
}
}