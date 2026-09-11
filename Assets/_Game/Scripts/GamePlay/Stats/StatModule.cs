using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Types;
using VContainer;

namespace _Game.Scripts.GamePlay
{
public abstract class StatModule : IDisposable
{
    private StatsContainer _statsContainer;

    private readonly Dictionary<StatType, Action<float>> _statHandlers = new();

    [Inject]
    private void Construct(StatsContainer statsContainer)
    {
        _statsContainer = statsContainer;
        _statsContainer.OnStatUpdated += StatContainerUpdated;

        Configure();
    }

    protected abstract void Configure();

    protected void BindStat(StatType type, Action<float> handler)
    {
        _statHandlers[type] = handler;
    }

    private void StatContainerUpdated(StatType type, float value)
    {
        if (_statHandlers.TryGetValue(type, out var handler))
        {
            handler(value);
        }
    }

    public virtual void Dispose()
    {
        _statsContainer.OnStatUpdated -= StatContainerUpdated;
    }
}
}