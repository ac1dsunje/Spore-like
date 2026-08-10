using _Game.Scripts.GamePlay.Player.Modules.Stats;
using _Game.Scripts.GamePlay.Stats;

namespace _Game.Scripts.GamePlay.Player.Modules.Attack
{
public class AttackModule: StatModule
{
    public float PhysicalDamage { get; private set; }
    public float AttackRange { get; private set; }

    public AttackModule(PlayerStats playerStats) : base(playerStats)
    {
        BindStat(StatType.PhysicalDamage, UpdatePhysicalDamage);
        BindStat(StatType.AttackRange, UpdateAttackRange);
    }

    private void UpdatePhysicalDamage(float value) => PhysicalDamage = value;
    private void UpdateAttackRange(float value) => AttackRange = value;
}
}