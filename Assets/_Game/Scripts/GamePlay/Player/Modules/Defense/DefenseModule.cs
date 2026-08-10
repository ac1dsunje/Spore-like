using System;
using _Game.Scripts.GamePlay.Player.Modules.Stats;
using _Game.Scripts.GamePlay.Stats;

namespace _Game.Scripts.GamePlay.Player.Modules.Defense
{
public class DefenseModule: StatModule
{
    public float DamageResistance => _damageResistance / 100f;
    
    public float DamageReflection => _damageReflection / 100f;
    private float _damageReflection;
    public event Action<float> OnDamageReflected;
    private float _damageResistance;
    public event Action<float> OnDamageResisted;

    public DefenseModule(PlayerStats playerStats) : base(playerStats)
    {
        BindStat(StatType.DamageReflection, UpdateDamageReflection);
        BindStat(StatType.DamageResistance, UpdateDamageResistance);
    }

    public float ApplyResistance(float damage)
    {
        var resisted = damage * DamageResistance;
        OnDamageResisted?.Invoke(resisted);
        return damage - resisted;
    }
    
    public void ReflectDamage(float damage, IDamageAble damager)
    {
        var returnedDamage = damage * DamageReflection;
        OnDamageReflected?.Invoke(returnedDamage);
        damager.TakeDamage(returnedDamage, null);
    }

    private void UpdateDamageReflection(float newValue) => _damageReflection = newValue;
    
    private void UpdateDamageResistance(float newValue) => _damageResistance = newValue;
}
}