using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Types;
using UnityEngine;

namespace _Game.Scripts.GamePlay
{
[CreateAssetMenu(fileName = "NewStatsConfig", menuName = "Game/Stats/Config")]
public class StatsConfig: ScriptableObject
{
    [field: SerializeField] public List<Stat> Stats { get; private set; } = new();

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

        var existing = new Dictionary<StatType, Stat>();

        foreach (var stat in Stats)
        {
            existing.TryAdd(stat.Type, stat);
        }

        Stats.Clear();

        foreach (var type in enumValues)
        {
            Stats.Add(existing.TryGetValue(type, out var stat) ? stat : new Stat(type, 0f));
        }
    }
}
}