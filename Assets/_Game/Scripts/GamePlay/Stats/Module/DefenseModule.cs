using System;

namespace _Game.Scripts.GamePlay.Module
{
public class DefenseModule: StatModule
{
    public float DamageResistance { get; private set; }
    
    public float DamageReflection { get; private set; }
    
    public event Action<float> OnDamageReflected;
    public event Action<float> OnDamageResisted;

    protected override void Configure()
    {
        BindStat(StatType.DamageReflection, UpdateDamageReflection);
        BindStat(StatType.DamageResistance, UpdateDamageResistance);
    }

    public float ApplyResistance(float damage, float ignoreResistance)
    {
        var resistedPercent = MathF.Max(0, DamageResistance - ignoreResistance);
        var resisted = damage * resistedPercent;
        OnDamageResisted?.Invoke(resisted);
        return damage - resisted;
    }
    
    public float ReflectDamage(float damage)
    {
        var returnedDamage = damage * DamageReflection;
        OnDamageReflected?.Invoke(returnedDamage);
        return returnedDamage;
    }

    private void UpdateDamageReflection(float value) => DamageReflection = value / 100f;
    
    private void UpdateDamageResistance(float value) => DamageResistance = value / 100f;
}
}