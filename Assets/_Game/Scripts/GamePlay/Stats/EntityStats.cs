using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Types;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay
{
public class EntityStats: IStartable
{
    private readonly Dictionary<StatType, float> _stats = new();
    private readonly Dictionary<StatType, float> _basicStats = new();

    private readonly Dictionary<IStatSource, List<SourceStat>> _sourceStats = new();
    
    public event Action<StatType, float> OnStatUpdated;
    
    [Inject] private StatTypeConfig _config;
    [Inject] private StatsConfig _entityStatsConfig;
    
    public void Start()
    {
        AddInitialStats(_entityStatsConfig.Stats);
    }
    
    private void AddInitialStats(List<Stat> stats)
    {
        foreach (var stat in stats)
        {
            _basicStats[stat.Type] = stat.Value;
            _stats[stat.Type] = stat.Value;

            UpdateStat(stat.Type);
        }
    }

    public void AddSource(IStatSource source)
    {
        var stats = source.GetStats();
        
        if (_sourceStats.ContainsKey(source)) return;
        
        _sourceStats.Add(source, stats);
        
        foreach (var stat in stats)
        {
            RecalculateStat(stat.Type);
        }
    }

    public void UpdateSource(IStatSource source)
    {
        if (!_sourceStats.ContainsKey(source)) return;
        
        var changedTypes = new HashSet<StatType>();

        foreach (var stat in source.GetStats())
        {
            changedTypes.Add(stat.Type);
        }
        
        _sourceStats[source] = source.GetStats();


        foreach (var type in changedTypes)
        {
            RecalculateStat(type);
        }
    }
    
    public void RemoveSource(IStatSource source)
    {
        if (source == null) return;
        if (!_sourceStats.TryGetValue(source, out var stats)) return;

        var affectedStats = new HashSet<StatType>();

        foreach (var stat in stats)
        {
            affectedStats.Add(stat.Type);
        }

        _sourceStats.Remove(source);
        
        foreach (var type in affectedStats)
        {
            RecalculateStat(type);
        }
    }

    private void RecalculateStat(StatType type)
    {
        var baseValue = _basicStats.GetValueOrDefault(type);

        var stats = GetStats(type);
        
        var result = baseValue;

        foreach (var stat in stats)
        {
            if (stat.Operation != StatOperation.Add) continue;

            result += stat.CurrentValue;
        }

        foreach (var stat in stats)
        {
            if (stat.Operation != StatOperation.Multiply || stat.Target != StatTarget.Base) continue;

            result += baseValue * stat.CurrentValue;
        }

        foreach (var stat in stats)
        {
            if (stat.Operation != StatOperation.Percent || stat.Target != StatTarget.Base) continue;

            result += baseValue * stat.CurrentValue / 100f;
        }

        foreach (var stat in stats)
        {
            if (stat.Operation != StatOperation.Multiply || stat.Target != StatTarget.Total) continue;
            
            result *= stat.CurrentValue;
        }

        foreach (var stat in stats)
        {
            if (stat.Operation != StatOperation.Percent || stat.Target != StatTarget.Total) continue;
            
            result *= 1f + stat.CurrentValue / 100f;
        }

        result = _config.Clamp(type, result);

        _stats[type] = result;
        
        UpdateStat(type);
    }


    private List<SourceStat> GetStats(StatType type)
    {
        var result = new List<SourceStat>();

        foreach (var source in _sourceStats.Values)
        {
            foreach (var stat in source)
            {
                if (stat.Type == type)
                {
                    result.Add(stat);
                }
            }
        }

        return result;
    }


    private void UpdateStat(StatType type)
    {
        if (_stats.TryGetValue(type, out var value))
        {
            OnStatUpdated?.Invoke(type, value);
        }
    }
}
}