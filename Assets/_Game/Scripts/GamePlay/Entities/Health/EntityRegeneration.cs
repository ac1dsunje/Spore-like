using System;
using System.Collections;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities.Health
{
public class EntityRegeneration: IStartable, IDisposable
{
    private const string RegenerationKey = "Regeneration";
    private const string WaitKey = "WaitBeforeRegeneration";

    [Inject] private HealthModule _health;
    [Inject] private CoroutineRunner _runner;

    public void Start()
    {
        _health.OnDamageTaken += StopRegeneration;
    }

    private void StartRegeneration()
    {
        if (_health.Regeneration <= 0f) return;
        _runner.Run(RegenerationKey, Regenerate());
    }

    private void StopRegeneration(float damage)
    {
        if (_health.Regeneration <= 0f) return;
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