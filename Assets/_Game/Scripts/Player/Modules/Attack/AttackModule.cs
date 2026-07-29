using System;
using _Game.Scripts.Evolutions.Stats;
using _Game.Scripts.Player.Modules.Stats;

namespace _Game.Scripts.Player.Modules.Attack
{
public class AttackModule: StatModule
{
    private float _damageReflection;
    public event Action<int> OnDamageReflected;
    private float _physicalDamage;

    public AttackModule(PlayerStats playerStats): base(playerStats) {}

    protected override void PlayerStatUpdated(StatType type, float value)
    {
        switch (type)
        {
            case StatType.DamageReflection:
                UpdateDamageReflection(value);
                break;
            case StatType.PhysicalDamage:
                UpdatePhysicalDamage(value);
                break;
        }
    }
    
    public void ReflectDamage(float damage, IDamageAble damager)
    {
        var returnedDamage = damage * _damageReflection;
        if (returnedDamage >= 1f) OnDamageReflected?.Invoke((int)returnedDamage);
        damager.TakeDamage(returnedDamage, null);
    }

    private void UpdateDamageReflection(float newValue) => _damageReflection = newValue;

    private void UpdatePhysicalDamage(float newValue) => _physicalDamage = newValue;
}
}