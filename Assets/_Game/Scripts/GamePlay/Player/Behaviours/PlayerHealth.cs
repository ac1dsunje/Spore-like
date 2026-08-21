using System.Collections;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Player.Network;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Behaviours
{
public class PlayerHealth: EntityNetworkBehaviour, IDamageAble
{
    private HealthModule _health;
    private DefenseModule _defense;
    private AttackModule _attack;
    
    [Inject]
    private void Construct(HealthModule health, DefenseModule defense, AttackModule attack)
    {
        _health = health;
        _defense = defense;
        _attack = attack;

        _health.OnDamageTaken += StopRegeneration;
        _health.OnDeath += Die;
    }

    public float TakeDamage(HitInfo hit)
    {
        var damage = _defense.ApplyResistance(hit.Damage, hit.IgnoreResistance);
        _health.TakeDamage(damage);
        var returnedDamage = _defense.ReflectDamage(damage);
        var returnedHit = new HitInfo(returnedDamage, _attack.IgnoreResistance, null);
        hit.Owner?.TakeDamage(returnedHit);
        return damage;
    }

    public void SetDamageDealt(float damage) => _attack.SetDamageDealt(damage);

    private void StartRegeneration() => StartCoroutine(Regenerate());

    private void StopRegeneration(float damage)
    {
        StopAllCoroutines();
        StartCoroutine(WaitBeforeRegeneration());
    }

    private IEnumerator Regenerate()
    {
        while (true)
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
        if (!IsLocal)
        {
            Destroy(gameObject);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        StopAllCoroutines();

        if (_health == null) return;

        _health.OnDamageTaken -= StopRegeneration;
        _health.OnDeath -= Die;
    }
}
}