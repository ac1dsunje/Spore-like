using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Evolutions;
using _Game.Scripts.GamePlay.Stats;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Player.Modules.Stats
{
public class PlayerStats
{
    //Stats
    private readonly Dictionary<StatType, float> _stats = new();
    private readonly Dictionary<StatType, float> _basicStats = new();
    private readonly Dictionary<Evolution, Dictionary<StatType, float>> _evolutionStats = new();

    public event Action<StatType, float> OnStatUpdated;


    public void Initialize(StatsConfig[] configs)
    {
        foreach (var config in configs)
        {
            AddInitialStats(config.Stats);
        }
    }

    public void AddEvolution(Evolution evolution)
    {
        AddEvolutionStats(evolution);

        foreach (var stat in evolution.Stats)
        {
            RecalculateStat(stat.Type);
        }
    }

    public void UpdateEvolution(Evolution evolution)
    {
        var changedStats = new HashSet<StatType>();

        foreach (var stat in evolution.Stats)
        {
            if (!_evolutionStats[evolution].ContainsKey(stat.Type) ||
                !Mathf.Approximately(_evolutionStats[evolution][stat.Type], stat.CurrentValue))
            {
                changedStats.Add(stat.Type);
            }
            
            _evolutionStats[evolution][stat.Type] = stat.CurrentValue;
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

    private void AddEvolutionStats(Evolution evolution)
    {
        if (_evolutionStats.ContainsKey(evolution))
            return;

        var stats = new Dictionary<StatType, float>();

        foreach (var stat in evolution.Stats)
        {
            stats.Add(stat.Type, stat.CurrentValue);
        }

        _evolutionStats.Add(evolution, stats);
    }

    private void RecalculateStat(StatType type)
    {
        var value = _basicStats.GetValueOrDefault(type, 0f);

        foreach (var evolution in _evolutionStats)
        {
            if (evolution.Value.TryGetValue(type, out var statValue))
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