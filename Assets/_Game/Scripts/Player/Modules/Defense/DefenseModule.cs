using System;
using _Game.Scripts.Player.Modules.Stats;
using _Game.Scripts.Stats;

namespace _Game.Scripts.Player.Modules.Defense
{
public class DefenseModule: StatModule
{
    public float DamageResistance => _damageResistance / 100f;
    
    private float _damageReflection;
    public event Action<int> OnDamageReflected;
    private float _damageResistance;
    public event Action<int> OnDamageResisted;

    public DefenseModule(PlayerStats playerStats): base(playerStats) {}

    protected override void PlayerStatUpdated(StatType type, float value)
    {
        switch (type)
        {
            case StatType.DamageReflection:
                UpdateDamageReflection(value);
                break;
            
            case StatType.DamageResistance:
                UpdateDamageResistance(value);
                break;
        }
    }

    public float GetDamageAfterResistance(float value)
    {
        var resisted = value * DamageResistance;
        if (resisted >= 1f) OnDamageResisted?.Invoke((int)resisted);
        return value - resisted;
    }
    
    public void ReflectDamage(float damage, IDamageAble damager)
    {
        var returnedDamage = damage * _damageReflection;
        if (returnedDamage >= 1f) OnDamageReflected?.Invoke((int)returnedDamage);
        damager.TakeDamage(returnedDamage, null);
    }

    private void UpdateDamageReflection(float newValue) => _damageReflection = newValue;
    
    private void UpdateDamageResistance(float newValue) => _damageResistance = newValue;
}
}