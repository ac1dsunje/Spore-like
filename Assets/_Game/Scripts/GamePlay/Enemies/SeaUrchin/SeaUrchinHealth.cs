using System;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Entities.Hitboxes;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Enemies.SeaUrchin
{
public class SeaUrchinHealth: IStartable, IDisposable
{
    [Inject] private HealthModule _healthModule;
    [Inject] private DefenseModule _defenseModule;
    [Inject] private BodyHitbox _hitbox;
    [Inject] private EntitiesRegistry _entitiesRegistry;
    [Inject] private IDamageSource _damageSource;

    public void Start()
    {
        _healthModule.OnDeath += Die;
        _hitbox.OnHit += TakeDamage;
    }

    private void TakeDamage(HitInfo hit)
    {
        var damage = _defenseModule.ApplyResistance(hit.Damage, hit.IgnoreResistance);
        _healthModule.TakeDamage(damage);
        var returnedDamage = _defenseModule.ReflectDamage(damage);
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
        _healthModule.OnDeath -= Die;
        _hitbox.OnHit -= TakeDamage;
    }
}
}