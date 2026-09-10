using System;
using System.Collections.Generic;
using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Buffs;
using _Game.Scripts.GamePlay.Modules;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities
{
public class BuffsModule : IStartable, IDisposable
{
    public event Action<Buff> OnBuffActivated;
    public event Action<Buff> OnBuffDeactivated;
    
    private readonly Dictionary<BuffType, Buff> _buffs = new();
    private readonly List<Buff> _activeBuffs = new();

    private readonly EntityStats _stats;
    private readonly HealthModule _health;
    private readonly BuffsDatabase _dataDatabase;
    private readonly Ticker _ticker;

    public BuffsModule(EntityStats stats, HealthModule health, BuffsDatabase dataDatabase, Ticker ticker)
    {
        _stats = stats;
        _health = health;
        _dataDatabase = dataDatabase;
        _ticker = ticker;
    }
    
    public void Start()
    {
        RegisterBuffs();
        _ticker.OnTick += Tick;
    }

    private void RegisterBuffs()
    {
        foreach (var buffConfig in _dataDatabase.Buffs)
        {
            _buffs[buffConfig.Type] = new Buff(_stats, _health, buffConfig);
        }
    }

    private void Tick(float deltaTime)
    {
        for (var i = _activeBuffs.Count - 1; i >= 0; i--)
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