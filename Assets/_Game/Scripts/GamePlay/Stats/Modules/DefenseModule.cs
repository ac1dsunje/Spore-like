using System;
using _Game.Scripts.GamePlay.Types;

namespace _Game.Scripts.GamePlay.Modules
{
public class DefenseModule : StatModule
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
        var rawResisted = _damageResistance - ignoreResistance;
        var resisted = MathF.Max(0f, MathF.Min(1f, rawResisted));
        var finalDamage = damage * (1f - resisted);
    
        if (resisted > 0)
        {
            OnDamageResisted?.Invoke(damage - finalDamage);
        }
        return finalDamage;
    }
    
    public float ReflectDamage(float damage)
    {
        var returnedDamage = damage * _damageReflection;
        OnDamageReflected?.Invoke(returnedDamage);
        return returnedDamage;
    }

    private void UpdateDamageReflection(float value) => _damageReflection = value / 100f;
    
    private void UpdateDamageResistance(float value) => _damageResistance = value;
}
}