using System;
using System.Collections.Generic;
using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Buffs;
using _Game.Scripts.GamePlay.Buffs.Types;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
public class BuffsModule: IStartable, IDisposable
{
    public event Action<Buff> OnBuffActivated;
    public event Action<Buff> OnBuffDeactivated;
    
    private readonly Dictionary<BuffType, Buff> _buffs = new();
    private readonly List<Buff> _activeBuffs = new();

    [Inject] private EntityStats _stats;
    [Inject] private HealthModule _health;
    [Inject] private BuffsDatabase _dataDatabase;
    [Inject] private Ticker _ticker;
    
    public void Start()
    {
        RegisterBuffs();
        _ticker.OnTick += Tick;
    }

    private void RegisterBuffs()
    {
        foreach (var buffConfig in _dataDatabase.Buffs)
        {
            Buff buff = buffConfig.Type switch
            {
                BuffType.Suffocating => new SuffocatingDebuff(_stats, _health, buffConfig),
                BuffType.BadPassAbility => new BadPassAbility(_stats, buffConfig),
                BuffType.Heat => new HeatDebuff(_stats, _health, buffConfig),
                BuffType.Cold => new ColdDebuff(_stats, _health, buffConfig),
                BuffType.Starvation => new StarvationDebuff(_stats, _health, buffConfig),
                BuffType.Overeating => new OvereatingDebuff(_stats, _health, buffConfig),
                _ => null
            };

            if (buff != null)
                _buffs[buffConfig.Type] = buff;
            else
                Debug.Log($"Buff with type {buffConfig.Type} is not implemented");
        }
    }

    private void Tick(float deltaTime)
    {
        for (int i = _activeBuffs.Count - 1; i >= 0; i--)
        {
            _activeBuffs[i].Do(deltaTime);
        }
    }

    public void Set(BuffType type, bool state)
    {
        if (!_buffs.TryGetValue(type, out var currentBuff)) return;

        if (state && !currentBuff.IsActive)
        {
            currentBuff.Activate();
            _activeBuffs.Add(currentBuff);
            OnBuffActivated?.Invoke(currentBuff);
        }
        else if (!state && currentBuff.IsActive)
        {
            currentBuff.Deactivate();
            _activeBuffs.Remove(currentBuff);
            OnBuffDeactivated?.Invoke(currentBuff);
        }
    }
    
    public void Dispose()
    {
        _ticker.OnTick -= Tick;
    }
}
}