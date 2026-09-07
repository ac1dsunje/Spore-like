using System;
using _Game.Scripts.GamePlay.Types;

namespace _Game.Scripts.GamePlay.Modules
{
public class DefenseModule: StatModule
{
    private float _damageResistance;
    private float _damageReflection;
    
    public event Action<float> OnDamageReflected;
    public event Action<float> OnDamageResisted;

    protected override void Configure()
    {
        BindStat(StatType.DamageReflection, UpdateDamageReflection);
        BindStat(StatType.DamageResistance, UpdateDamageResistance);
    }

    public float ApplyResistance(float damage, float ignoreResistance)
    {
        var resistedPercent = MathF.Max(0, _damageResistance - ignoreResistance);
        var resisted = damage * resistedPercent;
        OnDamageResisted?.Invoke(resisted);
        return damage - resisted;
    }
    
    public float ReflectDamage(float damage)
    {
        var returnedDamage = damage * _damageReflection;
        OnDamageReflected?.Invoke(returnedDamage);
        return returnedDamage;
    }

    private void UpdateDamageReflection(float value) => _damageReflection = value / 100f;
    
    private void UpdateDamageResistance(float value) => _damageResistance = value / 100f;
}
}