using System.Collections.Generic;
using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Interfaces;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Buffs
{
public abstract class Buff: IStatSource
{
    public BuffType Type => _config.Type;
    public string Name => _config.Name;
    public Sprite Sprite => _config.Sprite;
    
    public List<SourceStat> GetStats() => _config.Stats;
    
    public bool IsActive => _isActive;
    
    private readonly BuffConfig _config;
    private readonly  Ticker _ticker;
    private readonly EntityStats _entityStats;

    private bool _isActive;

    protected Buff(EntityStats entityStats, BuffConfig config, Ticker ticker)
    {
        _entityStats = entityStats;
        _config = config;
        _ticker = ticker;
        _ticker.OnTick += OnTick;
    }

    private void OnTick(float timeDelta)
    {
        if (_isActive) Do(timeDelta);
    }

    protected virtual void Do(float timeDelta) { }

    public void Activate()
    {
        _isActive = true;
        _entityStats.AddSource(this);
    }

    public void Deactivate()
    {
        _isActive = false;
        _entityStats.RemoveSource(this);
    }
}
}