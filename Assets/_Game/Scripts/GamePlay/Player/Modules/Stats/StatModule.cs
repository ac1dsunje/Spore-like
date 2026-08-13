using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Stats;

namespace _Game.Scripts.GamePlay.Player.Modules.Stats
{
public abstract class StatModule : IDisposable
{
    private readonly PlayerStats _playerStats;

    private readonly Dictionary<StatType, Action<float>> _statHandlers = new();

    protected StatModule(PlayerStats playerStats)
    {
        _playerStats = playerStats;
        _playerStats.OnStatUpdated += PlayerStatUpdated;
    }

    protected void BindStat(StatType type, Action<float> handler)
    {
        _statHandlers[type] = handler;
    }

    private void PlayerStatUpdated(StatType type, float value)
    {
        if (_statHandlers.TryGetValue(type, out var handler))
        {
            handler(value);
        }
    }

    public virtual void Dispose()
    {
        _playerStats.OnStatUpdated -= PlayerStatUpdated;
    }
}
}