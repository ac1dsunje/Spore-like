using _Game.Scripts.GamePlay.Player.Modules.Stats;
using _Game.Scripts.GamePlay.Stats;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Modules.Attack
{
public class AttackModule: StatModule
{
    public float PhysicalDamage { get; private set; }
    public float AttackRange { get; private set; }
    public float IgnoreResistance { get; private set; }

    public IDamageAble Owner { get; private set; }

    [Inject]
    public AttackModule(PlayerStats playerStats) : base(playerStats)
    {
        BindStat(StatType.PhysicalDamage, UpdatePhysicalDamage);
        BindStat(StatType.AttackRange, UpdateAttackRange);
        BindStat(StatType.IgnoreDamageResistance, UpdateIgnoreResistance);
    }

    public void SetOwner(IDamageAble owner) => Owner = owner;
    private void UpdatePhysicalDamage(float value) => PhysicalDamage = value;
    private void UpdateAttackRange(float value) => AttackRange = value;
    private void UpdateIgnoreResistance(float value) => IgnoreResistance = value;
}
}