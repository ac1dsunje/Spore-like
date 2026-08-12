using System;
using _Game.Scripts.GamePlay.Player.Modules.Stats;
using _Game.Scripts.GamePlay.Stats;
using VContainer;

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

    private HitInfo _returnedHit;

    [Inject]
    public DefenseModule(PlayerStats playerStats) : base(playerStats)
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

    private void UpdateDamageReflection(float value) => _damageReflection = value;
    
    private void UpdateDamageResistance(float value) => _damageResistance = value;
}
}