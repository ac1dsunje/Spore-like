using _Game.Scripts.GamePlay.Player.Modules.Stats;
using _Game.Scripts.GamePlay.Stats;

namespace _Game.Scripts.GamePlay.Player.Modules.Attack
{
public class AttackModule: StatModule
{
    public float PhysicalDamage { get; private set; }

    public AttackModule(PlayerStats playerStats) : base(playerStats)
    {
        BindStat(StatType.PhysicalDamage, UpdatePhysicalDamage);
    }

    private void UpdatePhysicalDamage(float newValue) => PhysicalDamage = newValue;
}
}