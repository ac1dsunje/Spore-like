using System;
using System.Collections;
using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Buffs;
using _Game.Scripts.GamePlay.Modules;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
public class EntityStomach : IStartable, IDisposable
{
    private readonly StomachModule _stomach;
    private readonly BuffsModule _buffs;
    private readonly CoroutineRunner _coroutineRunner;
    private readonly EntityScope _scope;
    
    private float _currentValue;
    private float _maxValue;

    private const float LoseHungerTime = 5f;
    private const string HungerCoroutineKey = "HungerLoop";

    public EntityStomach(StomachModule stomach, BuffsModule buffs, CoroutineRunner coroutineRunner, EntityScope scope)
    {
        _stomach = stomach;
        _buffs = buffs;
        _coroutineRunner = coroutineRunner;
        _scope = scope;
    }
    
    public void Start()
    {
        _stomach.Current.CombineLatest(_stomach.Max, (current, max) => (current, max))
            .Subscribe(x => UpdateBuffs(x.current, x.max))
            .AddTo(_scope);
        
        _coroutineRunner.Run(this, HungerCoroutineKey, HungerLoop());
    }
    
    private IEnumerator HungerLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(LoseHungerTime);
            _stomach.Reduce(1);
        }
    }

    private void UpdateBuffs(float current, float max)
    {
        _buffs.Set(BuffType.Overeating, current > max);
        _buffs.Set(BuffType.Starvation, current <= 0f);
    }

    public void Dispose()
    {
        if (_coroutineRunner != null && _coroutineRunner.gameObject != null)
        {
            _coroutineRunner.Stop(this);
        }
    }
}
}