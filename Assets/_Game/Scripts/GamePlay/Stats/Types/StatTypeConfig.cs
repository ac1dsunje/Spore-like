using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Types
{
[CreateAssetMenu(fileName = "Stat type config", menuName = "Configs/Game/Stats/Types")]
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

    private StatTypeData Get(StatType type)
    {
        return Types.FirstOrDefault(data => data.Type == type);
    }

    public string GetName(StatType type)
    {
        return Get(type).Name;
    }

    public float Clamp(StatType type, float value)
    {
        var data = Get(type);

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