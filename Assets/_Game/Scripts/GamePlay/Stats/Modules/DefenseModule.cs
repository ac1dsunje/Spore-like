using _Game.Scripts.GamePlay.Types;
using R3;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Modules
{
public class DefenseModule : StatModule
{
    public ReadOnlyReactiveProperty<float> Resisted => _resisted;
    public ReadOnlyReactiveProperty<float> Reflected => _reflected;
        
    private readonly ReactiveProperty<float> _resisted = new();
    private readonly ReactiveProperty<float> _reflected = new();
    
    private float _damageResistance;
    private float _damageReflection;
    
    protected override void Configure()
    {
        BindStat(StatType.DamageReflection, UpdateDamageReflection);
        BindStat(StatType.DamageResistance, UpdateDamageResistance);
    }
    
    public float ApplyResistance(float damage, float ignoreResistance)
    {
        var resisted = Mathf.Clamp01(_damageResistance - ignoreResistance);
        var finalDamage = damage * (1f - resisted);
        
        _resisted.Value += damage - finalDamage;
        
        return finalDamage;
    }
    
    public float ReflectDamage(float damage)
    {
        var returnedDamage = damage * _damageReflection;
        _reflected.Value += returnedDamage;
        return returnedDamage;
    }

    private void UpdateDamageReflection(float value) => _damageReflection = value / 100f;
    private void UpdateDamageResistance(float value) => _damageResistance = value;
}
}