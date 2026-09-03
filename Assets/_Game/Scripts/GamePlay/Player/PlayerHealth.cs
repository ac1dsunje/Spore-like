using System;
using System.Collections;
using _Game.Scripts.GamePlay.Entities.Health;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Player
{
public class PlayerHealth : IStartable, IDisposable, IHealthController
{
    private const string RegenerationKey = "Regeneration";
    private const string WaitKey = "WaitBeforeRegeneration";

    [Inject] private HealthModule _health;
    [Inject] private DefenseModule _defense;
    [Inject] private CoroutineRunner _runner;
    [Inject] private IDamageSource _damageSource;

    public void Start()
    {
        _health.OnDamageTaken += StopRegeneration;
    }

    public void TakeDamage(HitInfo hit)
    {
        var damage = _defense.ApplyResistance(hit.Damage, hit.IgnoreResistance);
        _health.TakeDamage(damage);
        var returnedDamage = _defense.ReflectDamage(damage);
        var returnedHit = new HitInfo(returnedDamage, 0, _damageSource, null);
        hit.Receiver?.TakeDamage(returnedHit);
        hit.Source?.SetDamageDealt(damage);
    }

    private void StartRegeneration()
    {
        _runner.Run(RegenerationKey, Regenerate());
    }

    private void StopRegeneration(float damage)
    {
        _runner.Stop(RegenerationKey);
        _runner.Stop(WaitKey);
        _runner.Run(WaitKey, WaitBeforeRegeneration());
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

    public void Dispose()
    {
        _runner.Stop(RegenerationKey);
        _runner.Stop(WaitKey);

        _health.OnDamageTaken -= StopRegeneration;
    }
}
}