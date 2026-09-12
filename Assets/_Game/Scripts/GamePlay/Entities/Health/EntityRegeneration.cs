using System;
using System.Threading;
using _Game.Scripts.GamePlay.Modules.Health;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities.Health
{
public class EntityRegeneration : IStartable, IDisposable
{
    private readonly HealthModule _health;
    private readonly RegenerationModule _regeneration;
    
    private IDisposable _subscription;

    private readonly CancellationTokenSource _lifetimeCts = new();
    
    private CancellationTokenSource _cycleCts;

    public EntityRegeneration(HealthModule health, RegenerationModule regeneration)
    {
        _health = health;
        _regeneration = regeneration;
    }

    public void Start()
    {
        _subscription = _health.Current
            .Pairwise()
            .Subscribe(pair =>
            {
                if (pair.Previous > pair.Current)
                {
                    RestartRegenerationCycle();
                }
            });
    }

    private void RestartRegenerationCycle()
    {
        if (_regeneration.Current <= 0f) return;

        _cycleCts?.Cancel();
        _cycleCts?.Dispose();

        _cycleCts = CancellationTokenSource.CreateLinkedTokenSource(_lifetimeCts.Token);

        RegenerationCycleAsync(_cycleCts.Token).Forget();
    }

    private async UniTaskVoid RegenerationCycleAsync(CancellationToken token)
    {
        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: token);

            while (!Mathf.Approximately(_health.Current.CurrentValue, _health.Max.CurrentValue))
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: token);
                
                _health.Add(_regeneration.Current);
            }
        }
        catch (OperationCanceledException)
        {
            
        }
    }

    public void Dispose()
    {
        _subscription?.Dispose();

        _cycleCts?.Cancel();
        _cycleCts?.Dispose();

        _lifetimeCts.Cancel();
        _lifetimeCts.Dispose();
    }
}
}