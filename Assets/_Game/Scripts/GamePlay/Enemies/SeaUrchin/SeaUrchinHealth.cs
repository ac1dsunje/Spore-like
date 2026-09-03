using System;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Entities.Health;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Enemies.SeaUrchin
{
public class SeaUrchinHealth: IStartable, IDisposable, IHealthController
{
    [Inject] private HealthModule _health;
    [Inject] private DefenseModule _defense;
    [Inject] private EntitiesRegistry _entitiesRegistry;
    [Inject] private IDamageSource _damageSource;

    public void Start()
    {
        _health.OnDeath += Die;
    }

    public void TakeDamage(HitInfo hit)
    {
        var damage = _defense.ApplyResistance(hit.Damage, hit.IgnoreResistance);
        _health.TakeDamage(damage);
        var returnedDamage = _defense.ReflectDamage(damage);
        HitInfo returnedHit = new(returnedDamage, 0, _damageSource, null);
        hit.Receiver?.TakeDamage(returnedHit);
        hit.Source?.SetDamageDealt(damage);
    }

    private void Die(HealthModule health)
    {
        _entitiesRegistry.DestroyEntityByHealth(health);
    }

    public void Dispose()
    {
        _health.OnDeath -= Die;
    }
}
}