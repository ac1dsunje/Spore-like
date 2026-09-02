using System.Collections;
using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Entities.Hitboxes;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Behaviours
{
public class PlayerHealth: MonoBehaviour, IDamageReceiverController
{
    private HealthModule _health;
    private DefenseModule _defense;
    private IDamageSource _damageSource;
    private BodyHitbox _hitbox;
    
    [Inject]
    private void Construct(HealthModule health, DefenseModule defense, BodyHitbox hitbox)
    {
        _health = health;
        _defense = defense;
        _hitbox = hitbox;

        _health.OnDamageTaken += StopRegeneration;
        _health.OnDeath += Die;
        _hitbox.OnHit += TakeDamage;
    }

    public void SetDamageSource(IDamageSource source) => _damageSource = source;

    private void TakeDamage(HitInfo hit)
    {
        var damage = _defense.ApplyResistance(hit.Damage, hit.IgnoreResistance);
        _health.TakeDamage(damage);
        var returnedDamage = _defense.ReflectDamage(damage);
        var returnedHit = new HitInfo(returnedDamage, 0, _damageSource, null);
        hit.Receiver?.TakeDamage(returnedHit);
        hit.Source?.SetDamageDealt(damage);
    }

    private void StartRegeneration() => StartCoroutine(Regenerate());

    private void StopRegeneration(float damage)
    {
        StopAllCoroutines();
        StartCoroutine(WaitBeforeRegeneration());
    }

    private IEnumerator Regenerate()
    {
        while (_health.Health < _health.MaxHealth)
        {
            yield return new WaitForSeconds(1f);
            _health.Heal(_health.Regeneration);
        }
    }

    private IEnumerator WaitBeforeRegeneration()
    {
        yield return new WaitForSeconds(1f);
        StartRegeneration();
    }

    private void Die()
    {
        
    }

    private void OnDestroy()
    {
        StopAllCoroutines();

        if (_health == null) return;

        _health.OnDamageTaken -= StopRegeneration;
        _health.OnDeath -= Die;
        _hitbox.OnHit -= TakeDamage;
    }
}
}