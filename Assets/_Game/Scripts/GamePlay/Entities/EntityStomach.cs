using System;
using System.Threading;
using _Game.Scripts.GamePlay.Buffs;
using _Game.Scripts.GamePlay.Modules;
using Cysharp.Threading.Tasks;
using R3;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
public class EntityStomach : IStartable, IDisposable
{
    private readonly StomachModule _stomach;
    private readonly BuffsModule _buffs;
    private readonly EntityScope _scope;
    
    private float _currentValue;
    private float _maxValue;

    private const float LoseHungerTime = 5f;
    private CancellationTokenSource _cts;

    public EntityStomach(StomachModule stomach, BuffsModule buffs, EntityScope scope)
    {
        _stomach = stomach;
        _buffs = buffs;
        _scope = scope;
    }
    
    public void Start()
    {
        _stomach.Current.CombineLatest(_stomach.Max, (current, max) => (current, max))
            .Subscribe(x => UpdateBuffs(x.current, x.max))
            .AddTo(_scope);
        
        _cts = new CancellationTokenSource();
        HungerLoop(_cts.Token).Forget();
    }
    
    private async UniTaskVoid HungerLoop(CancellationToken token)
    {
        try
        {
            while (true)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(LoseHungerTime), cancellationToken: token);
                _stomach.Reduce(1);
            }
        }
        catch (OperationCanceledException)
        {
            
        }
        
    }

    private void UpdateBuffs(float current, float max)
    {
        _buffs.Set(BuffType.Overeating, current > max);
        _buffs.Set(BuffType.Starvation, current <= 0f);
    }

    public void Dispose()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;
    }
}
}