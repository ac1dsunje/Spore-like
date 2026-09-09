using _Game.Scripts.GamePlay.Modules;

namespace _Game.Scripts.GamePlay.Buffs.Types
{
public class OvereatingDebuff: Buff
{
    private readonly HealthModule _healthModule;
    
    public OvereatingDebuff(EntityStats entityStats, BuffConfig config)
        : base(entityStats, config)
    {
    }
}
}