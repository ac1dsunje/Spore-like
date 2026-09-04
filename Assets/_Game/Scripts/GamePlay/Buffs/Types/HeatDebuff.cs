using _Game.Scripts.GamePlay.Modules;

namespace _Game.Scripts.GamePlay.Buffs.Types
{
public class HeatDebuff: Buff
{
    private readonly HealthModule _healthModule;
    
    public HeatDebuff(EntityStats entityStats, HealthModule health, BuffConfig config)
        : base(entityStats, config)
    {
        _healthModule = health;
    }

    public override void Do(float deltatTime)
    {
        _healthModule.TakeDamage(deltatTime * 2f);
    }
}
}