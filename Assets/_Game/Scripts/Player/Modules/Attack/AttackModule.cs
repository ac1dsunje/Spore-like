using System;
using _Game.Scripts.Evolutions.Stats;

namespace _Game.Scripts.Player.Modules.Attack
{
public class AttackModule: StatModule
{
    public float DamageReflection { get; private set; }
    public event Action<int> OnDamageReflected;
    public float PhysicalDamage { get; private set; }

    public AttackModule(PlayerStats playerStats): base(playerStats) {}

    protected override void OnStatUpdated(EvolutionType type, float value)
    {
        switch (type)
        {
            case EvolutionType.DamageReflection:
                UpdateDamageReflection(value);
                break;
            case EvolutionType.PhysicalDamage:
                UpdatePhysicalDamage(value);
                break;
        }
    }
    
    public float ReflectDamage(float damage)
    {
        if (DamageReflection <= 0) return 0;

        OnDamageReflected?.Invoke(1);
        return damage * DamageReflection;
    }

    private void UpdateDamageReflection(float newValue) => DamageReflection = newValue;

    private void UpdatePhysicalDamage(float newValue) => PhysicalDamage = newValue;
}
}