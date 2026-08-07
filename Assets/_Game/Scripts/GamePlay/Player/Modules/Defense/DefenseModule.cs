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
    public event Action<int> OnDamageReflected;
    private float _damageResistance;
    public event Action<int> OnDamageResisted;

    public DefenseModule(PlayerStats playerStats) : base(playerStats)
    {
        BindStat(StatType.DamageReflection, UpdateDamageReflection);
        BindStat(StatType.DamageResistance, UpdateDamageResistance);
    }

    public float ApplyResistance(float damage)
    {
        var resisted = damage * DamageResistance;
        if (resisted >= 1f) OnDamageResisted?.Invoke((int)resisted);
        return damage - resisted;
    }
    
    public void ReflectDamage(float damage, IDamageAble damager)
    {
        var returnedDamage = damage * DamageReflection;
        if (returnedDamage >= 1f) OnDamageReflected?.Invoke((int)returnedDamage);
        damager.TakeDamage(returnedDamage, null);
    }

    private void UpdateDamageReflection(float newValue) => _damageReflection = newValue;
    
    private void UpdateDamageResistance(float newValue) => _damageResistance = newValue;
}
}