using System;
using System.Collections.Generic;
using VContainer;

namespace _Game.Scripts.GamePlay
{
public abstract class StatModule : IDisposable
{
    private EntityStats _entityStats;

    private readonly Dictionary<StatType, Action<float>> _statHandlers = new();

    [Inject]
    private void Construct(EntityStats entityStats)
    {
        _entityStats = entityStats;
        _entityStats.OnStatUpdated += EntityStatUpdated;

        Configure();
    }

    protected abstract void Configure();

    protected void BindStat(StatType type, Action<float> handler)
    {
        _statHandlers[type] = handler;
    }

    private void EntityStatUpdated(StatType type, float value)
    {
        if (_statHandlers.TryGetValue(type, out var handler))
        {
            handler(value);
        }
    }

    public virtual void Dispose()
    {
        _entityStats.OnStatUpdated -= EntityStatUpdated;
    }
}
}