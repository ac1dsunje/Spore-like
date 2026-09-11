using System;
using System.Collections;
using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Modules;
using R3;
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
    private IDisposable _subscription;

    public EntityRegeneration(HealthModule health, RegenerationModule regeneration, CoroutineRunner runner)
    {
        _health = health;
        _regeneration = regeneration;
        _runner = runner;
    }

    public void Start()
    {
        _subscription = _health.Current
            .Pairwise()
            .Subscribe(pair =>
            {
                var delta = pair.Previous - pair.Current;
                if (delta > 0)
                {
                    StopRegeneration();
                }
            });
    }

    private void StartRegeneration()
    {
        if (_regeneration.Current <= 0f) return;
        _runner.Run(this, RegenerationKey, Regenerate());
    }

    private void StopRegeneration()
    {
        if (_regeneration.Current <= 0f) return;
        _runner.Stop(this, RegenerationKey);
        _runner.Stop(this, WaitKey);
        _runner.Run(this, WaitKey, WaitBeforeRegeneration());
    }

    private IEnumerator Regenerate()
    {
        
        while (!Mathf.Approximately(_health.Current.CurrentValue, _health.Max.CurrentValue))
        {
            yield return new WaitForSeconds(1f);
            _health.Add(_regeneration.Current);
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

        _subscription?.Dispose();
    }
}
}