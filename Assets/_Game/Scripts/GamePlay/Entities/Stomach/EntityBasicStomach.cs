using System;
using System.Collections;
using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Buffs;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities.Stomach
{
public class EntityBasicStomach: IStartable, IDisposable
{
    [Inject] private StomachModule _stomach;
    [Inject] private BuffsModule _buffs;
    [Inject] private CoroutineRunner _coroutineRunner;

    private const float LoseHungerTime = 5f;
    private const string HungerCoroutineKey = "HungerLoop";
    
    public void Start()
    {
        _stomach.OnValueChanged += UpdateBuffs;
        _coroutineRunner.Run(HungerCoroutineKey, HungerLoop());
    }
    
    private IEnumerator HungerLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(LoseHungerTime);
            _stomach.LoseHunger(1);
        }
    }

    private void UpdateBuffs(float current, float max)
    {
        _buffs.Set(BuffType.Overeating, current > max);
        _buffs.Set(BuffType.Starvation, current <= 0f);
    }

    public void Dispose()
    {
        _stomach.OnValueChanged -= UpdateBuffs;
        
        if (_coroutineRunner != null && _coroutineRunner.gameObject != null)
        {
            _coroutineRunner.Stop(HungerCoroutineKey);
        }
    }
}
}