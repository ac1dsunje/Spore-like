using System;
using System.Collections;
using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities.Health
{
public class EntityRegeneration : IStartable, IDisposable
{
    private const string RegenerationKey = "Regeneration";
    private const string WaitKey = "WaitBeforeRegeneration";

    private readonly HealthModule _health;
    private readonly RegenerationModule _regeneration;
    private readonly CoroutineRunner _runner;

    public EntityRegeneration(HealthModule health, RegenerationModule regeneration, CoroutineRunner runner)
    {
        _health = health;
        _regeneration = regeneration;
        _runner = runner;
    }

    public void Start()
    {
        _health.OnDamageTaken += StopRegeneration;
    }

    private void StartRegeneration()
    {
        if (_regeneration.Current <= 0f) return;
        _runner.Run(this, RegenerationKey, Regenerate());
    }

    private void StopRegeneration(float damage)
    {
        if (_regeneration.Current <= 0f) return;
        _runner.Stop(this, RegenerationKey);
        _runner.Stop(this, WaitKey);
        _runner.Run(this, WaitKey, WaitBeforeRegeneration());
    }

    private IEnumerator Regenerate()
    {
        while (!_health.HasMaxHp)
        {
            yield return new WaitForSeconds(1f);
            _health.Heal(_regeneration.Current);
        }
    }

    private IEnumerator WaitBeforeRegeneration()
    {
        yield return new WaitForSeconds(1f);
        StartRegeneration();
    }

    public void Dispose()
    {
        _runner.Stop(this);

        _health.OnDamageTaken -= StopRegeneration;
    }
}
}