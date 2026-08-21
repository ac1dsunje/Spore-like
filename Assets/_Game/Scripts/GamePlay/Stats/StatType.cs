using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Game.Scripts.GamePlay
{
public enum StatType
{
    Inertia = 8,
    MoveSpeed = 0,
    MaxHealth = 10,
    DamageReflection = 4,
    VisionRadius = 1,
    Regeneration = 7,
    Acceleration = 3,
    PhysicalDamage = 6,
    EatingStrength = 11,
    EatingPenetration = 12,
    EatingTime = 13,
    MaxEndurance = 14,
    EnduranceRecovery = 15,
    SprintMultiplier = 16,
    DamageResistance = 17,
    DashPower = 18,
    ColdResistance = 19,
    HeatResistance = 20,
    MinimalLethalTemperature = 21,
    MinimalComfortableTemperature = 22,
    MaximumComfortableTemperature = 23,
    MaximumLethalTemperature = 24,
    AttackRange = 25,
    IgnoreDamageResistance = 26,
    Sensorics = 27,
    Disguise = 28,
    LightingRadius = 29,
    Passability = 30,
    XRay = 31,
    AttackSpeed = 32,
    DisguiseInRest = 33,
}

[CreateAssetMenu(fileName = "Stat type config", menuName = "Configs/Game/Stat/Types")]
public class StatTypeConfig : ScriptableObject
{
    [field: SerializeField] public List<StatTypeData> Types { get; private set; } = new();

#if UNITY_EDITOR
    private void OnValidate()
    {
        Synchronize();
    }
#endif

    [ContextMenu("Synchronize")]
    public void Synchronize()
    {
        var enumValues = (StatType[])Enum.GetValues(typeof(StatType));

        var existing = new Dictionary<StatType, StatTypeData>();

        foreach (var data in Types)
        {
            existing.TryAdd(data.Type, data);
        }

        Types.Clear();

        foreach (var type in enumValues)
        {
            Types.Add(existing.TryGetValue(type, out var data) ? data : new StatTypeData(type));
        }
    }

    public float Clamp(StatType type, float value)
    {
        var data = Types.FirstOrDefault(data => data.Type == type);

        return data == null ? value : Mathf.Clamp(value, data.MinimalValue, data.MaximalValue);
    }
}

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