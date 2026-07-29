using System;
using _Game.Scripts.Evolutions.Stats;
using _Game.Scripts.Player.Modules.Stats;

namespace _Game.Scripts.Player.Modules
{
public abstract class StatModule: IDisposable
{
    protected readonly PlayerStats PlayerStats;

    protected StatModule(PlayerStats playerStats)
    {
        PlayerStats = playerStats;
        PlayerStats.OnStatUpdated += PlayerStatUpdated;
    }

    protected abstract void PlayerStatUpdated(StatType type, float value);
    
    public virtual void Dispose()
    {
        PlayerStats.OnStatUpdated -= PlayerStatUpdated;   
    }
}
}