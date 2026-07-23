using System;
using _Game.Scripts.Evolutions.Stats;

namespace _Game.Scripts.Player.Modules
{
public abstract class StatModule: IDisposable
{
    protected readonly PlayerStats _stats;

    protected StatModule(PlayerStats stats)
    {
        _stats = stats;
        _stats.OnStatUpdated += OnStatUpdated;
    }

    protected abstract void OnStatUpdated(EvolutionType type, float value);
    
    public virtual void Dispose()
    {
        _stats.OnStatUpdated -= OnStatUpdated;   
    }
}
}