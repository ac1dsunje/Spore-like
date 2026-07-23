using System;
using _Game.Scripts.Evolutions.Stats;

namespace _Game.Scripts.Player.Modules.Attack
{
public class AttackModule: IDisposable
{
    public float DamageReflection { get; private set; }
    public float PhysicalDamage { get; private set; }

    private PlayerStats _stats;

    public AttackModule(PlayerStats playerStats)
    {
        _stats = playerStats;
        _stats.OnStatUpdated += OnStatUpdated;
    }

    private void OnStatUpdated(EvolutionType type, float value)
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
        return damage * DamageReflection / 100;
    }

    private void UpdateDamageReflection(float newValue) => DamageReflection = newValue;

    private void UpdatePhysicalDamage(float newValue) => PhysicalDamage = newValue;

    public void Dispose()
    {
        _stats.OnStatUpdated -= OnStatUpdated;
    }
}
}