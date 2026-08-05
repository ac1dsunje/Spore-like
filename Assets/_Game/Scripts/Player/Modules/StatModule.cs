using System;
using _Game.Scripts.Player.Modules.Stats;
using _Game.Scripts.Stats;

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