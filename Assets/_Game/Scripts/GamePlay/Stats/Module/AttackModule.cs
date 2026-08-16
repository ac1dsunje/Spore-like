using _Game.Scripts.GamePlay.Interfaces;

namespace _Game.Scripts.GamePlay.Module
{
public class AttackModule: StatModule
{
    public float PhysicalDamage { get; private set; }
    public float AttackRange { get; private set; }
    public float IgnoreResistance { get; private set; }
    public float AttackSpeed { get; private set; }

    public IDamageAble Owner { get; private set; }

    protected override void Configure()
    {
        BindStat(StatType.PhysicalDamage, UpdatePhysicalDamage);
        BindStat(StatType.AttackRange, UpdateAttackRange);
        BindStat(StatType.IgnoreDamageResistance, UpdateIgnoreResistance);
        BindStat(StatType.AttackSpeed, UpdateAttackSpeed);
    }

    public void SetOwner(IDamageAble owner) => Owner = owner;
    private void UpdatePhysicalDamage(float value) => PhysicalDamage = value;
    private void UpdateAttackRange(float value) => AttackRange = value;
    private void UpdateIgnoreResistance(float value) => IgnoreResistance = value;
    private void UpdateAttackSpeed(float value) => AttackSpeed = value;
}
}