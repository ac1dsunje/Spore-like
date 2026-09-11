using System.Collections.Generic;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules.Health;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Buffs
{
public class Buff : IStatSource
{
    public BuffType Type => _config.Type;
    public string Name => _config.Name;
    public Sprite Sprite => _config.Sprite;
        
    public List<SourceStat> GetStats() => _config.Stats;
        
    public bool IsActive { get; private set; }
        
    private readonly BuffConfig _config;
    private readonly EntityStats _entityStats;
    private readonly HealthModule _health;

    public Buff(EntityStats entityStats, HealthModule health, BuffConfig config)
    {
        _entityStats = entityStats;
        _health = health;
        _config = config;
    }

    public void Do(float timeDelta)
    {
        if (_config.DamagePerSecond > 0)
        {
            _health.Reduce(timeDelta * _config.DamagePerSecond);
        }
    }

    public void Activate()
    {
        IsActive = true;
        _entityStats.AddSource(this);
    }

    public void Deactivate()
    {
        IsActive = false;
        _entityStats.RemoveSource(this);
    }
}
}