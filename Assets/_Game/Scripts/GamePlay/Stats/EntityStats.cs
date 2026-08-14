using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.GamePlay
{
public class EntityStats
{
    private readonly Dictionary<StatType, float> _stats = new();
    private readonly Dictionary<StatType, float> _basicStats = new();
    private readonly Dictionary<IStatSource, Dictionary<StatType, float>> _sourceStats = new();

    public event Action<StatType, float> OnStatUpdated;
    
    public void Initialize(StatsConfig[] configs)
    {
        foreach (var config in configs)
        {
            AddInitialStats(config.Stats);
        }
    }

    public void AddSource(IStatSource source)
    {
        var sourceStats = source.GetStats();
        
        AddSourceStats(source, sourceStats);
        
        foreach (var stat in sourceStats)
        {
            RecalculateStat(stat.Type);
        }
    }

    public void UpdateSource(IStatSource source)
    {
        var changedStats = new HashSet<StatType>();

        foreach (var stat in source.GetStats())
        {
            if (!_sourceStats[source].ContainsKey(stat.Type) ||
                !Mathf.Approximately(_sourceStats[source][stat.Type], stat.Value))
            {
                changedStats.Add(stat.Type);
            }
            
            _sourceStats[source][stat.Type] = stat.Value;
        }

        foreach (var statType in changedStats)
        {
            RecalculateStat(statType);
        }
    }

    private void AddInitialStats(List<Stat> stats)
    {
        foreach (var stat in stats)
        {
            _basicStats.Add(stat.Type, stat.Value);
            _stats.Add(stat.Type, stat.Value);

            UpdateStat(stat.Type);
        }
    }

    private void AddSourceStats(IStatSource source, List<Stat> sourceStats)
    {
        if (_sourceStats.ContainsKey(source))
            return;

        var stats = new Dictionary<StatType, float>();

        foreach (var stat in sourceStats)
        {
            stats.Add(stat.Type, stat.Value);
        }

        _sourceStats.Add(source, stats);
    }

    private void RecalculateStat(StatType type)
    {
        var value = _basicStats.GetValueOrDefault(type, 0f);

        foreach (var sourceStat in _sourceStats)
        {
            if (sourceStat.Value.TryGetValue(type, out var statValue))
            {
                value += statValue;
            }
        }

        _stats[type] = value;

        UpdateStat(type);
    }

    private void UpdateStat(StatType type) => OnStatUpdated?.Invoke(type, _stats[type]);
}
}