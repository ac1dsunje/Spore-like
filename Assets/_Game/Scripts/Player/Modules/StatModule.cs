using System;
using _Game.Scripts.Evolutions.Stats;
using _Game.Scripts.Player.Modules.Stats;

namespace _Game.Scripts.Player.Modules
{
public abstract class StatModule: IDisposable
{
    protected readonly PlayerStatsModule PlayerStatsModule;

    protected StatModule(PlayerStatsModule playerStatsModule)
    {
        PlayerStatsModule = playerStatsModule;
        PlayerStatsModule.OnStatUpdated += PlayerStatModuleUpdated;
    }

    protected abstract void PlayerStatModuleUpdated(StatType type, float value);
    
    public virtual void Dispose()
    {
        PlayerStatsModule.OnStatUpdated -= PlayerStatModuleUpdated;   
    }
}
}