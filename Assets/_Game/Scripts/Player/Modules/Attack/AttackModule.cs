using System;
using _Game.Scripts.Evolutions.Stats;
using _Game.Scripts.Player.Modules.Stats;

namespace _Game.Scripts.Player.Modules.Attack
{
public class AttackModule: StatModule
{
    public float DamageReflection { get; private set; }
    public event Action<int> OnDamageReflected;
    public float PhysicalDamage { get; private set; }

    public AttackModule(PlayerStatsModule playerStatsModule): base(playerStatsModule) {}

    protected override void PlayerStatModuleUpdated(StatType type, float value)
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
    
    public float ReflectDamage(float damage)
    {
        if (damage < 1f) return 0f;

        OnDamageReflected?.Invoke(1);
        var returnedDamage = damage * DamageReflection;
        return returnedDamage;
    }

    private void UpdateDamageReflection(float newValue) => DamageReflection = newValue;

    private void UpdatePhysicalDamage(float newValue) => PhysicalDamage = newValue;
}
}