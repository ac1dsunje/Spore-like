using System;
using _Game.Scripts.GamePlay.Types;

namespace _Game.Scripts.GamePlay.Modules
{
public class AttackModule: StatModule
{
    public float PhysicalDamage { get; private set; }
    public float IgnoreResistance { get; private set; }
    public float AttackTime { get; private set; }
    public event Action<float> OnDamageDealt;

    protected override void Configure()
    {
        BindStat(StatType.PhysicalDamage, UpdatePhysicalDamage);
        BindStat(StatType.IgnoreDamageResistance, UpdateIgnoreResistance);
        BindStat(StatType.AttackTime, UpdateAttackSpeed);
    }

    public void SetDamageDealt(float damage) => OnDamageDealt?.Invoke(damage);
    private void UpdatePhysicalDamage(float value) => PhysicalDamage = value;
    private void UpdateIgnoreResistance(float value) => IgnoreResistance = value;
    private void UpdateAttackSpeed(float value) => AttackTime = value;
}
}