using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Modules;

namespace _Game.Scripts.GamePlay.Buffs.Types
{
public class ColdDebuff: Buff
{
    private readonly HealthModule _healthModule;
    
    public ColdDebuff(EntityStats entityStats, HealthModule health, Ticker ticker, BuffConfig config)
        : base(entityStats, config, ticker)
    {
        _healthModule = health;
    }

    protected override void Do(float deltatTime)
    {
        _healthModule.TakeDamage(deltatTime * 1f);
    }
}
}