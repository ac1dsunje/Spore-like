using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Enemies.SeaUrchin
{
public class SeaUrchinHealth: MonoBehaviour, IDamageAble
{
    private HealthModule _healthModule;
    private DefenseModule _defenseModule;
    private AttackModule _attackModule;

    [Inject]
    private void Construct(DefenseModule defenseModule, HealthModule healthModule, AttackModule attackModule)
    {
        _defenseModule = defenseModule;
        _healthModule = healthModule;
        _attackModule = attackModule;

        _healthModule.OnDeath += Die;
    }

    public float TakeDamage(HitInfo hit)
    {
        var damage = _defenseModule.ApplyResistance(hit.Damage, hit.IgnoreResistance);
        _healthModule.TakeDamage(damage);
        var returnedDamage = _defenseModule.ReflectDamage(damage);
        HitInfo returnedHit = new(returnedDamage, _attackModule.IgnoreResistance, null);
        hit.Owner?.TakeDamage(returnedHit);
        return damage;
    }

    public void SetDamageDealt(float damage) {}

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _healthModule.OnDeath -= Die;
    }
}
}