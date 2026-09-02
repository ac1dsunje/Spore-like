using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Entities.Hitboxes;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Enemies.SeaUrchin
{
public class SeaUrchinHealth: MonoBehaviour, IDamageReceiverController
{
    private HealthModule _healthModule;
    private DefenseModule _defenseModule;
    private IDamageSource _damageSource;
    private EntityBodyHitbox _hitbox;

    [Inject]
    private void Construct(DefenseModule defenseModule, HealthModule healthModule, EntityBodyHitbox hitbox)
    {
        _defenseModule = defenseModule;
        _healthModule = healthModule;
        _hitbox = hitbox;

        _healthModule.OnDeath += Die;
        _hitbox.OnHit += TakeDamage;
    }

    public void SetDamageSource(IDamageSource source) => _damageSource = source;

    private void TakeDamage(HitInfo hit)
    {
        var damage = _defenseModule.ApplyResistance(hit.Damage, hit.IgnoreResistance);
        _healthModule.TakeDamage(damage);
        var returnedDamage = _defenseModule.ReflectDamage(damage);
        HitInfo returnedHit = new(returnedDamage, 0, _damageSource, null);
        hit.Receiver?.TakeDamage(returnedHit);
        hit.Source?.SetDamageDealt(damage);
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _healthModule.OnDeath -= Die;
        _healthModule.OnDeath -= Die;
    }
}
}