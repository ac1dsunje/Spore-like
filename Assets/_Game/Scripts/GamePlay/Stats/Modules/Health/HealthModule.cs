using System;
using _Game.Scripts.GamePlay.Types;
using R3;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Modules.Health
{
public class HealthModule : StatModule
{
    public ReadOnlyReactiveProperty<float> Current => _current;
    public ReadOnlyReactiveProperty<float> Max => _max;
    
    private readonly ReactiveProperty<float> _current = new();
    private readonly ReactiveProperty<float> _max = new();
    public event Action OnHitTaken;

    protected override void Configure()
    {
        BindStat(StatType.MaxHealth, UpdateMaxHealth);
    }

    public void Reset()
    {
        _current.Value = _max.Value;
    }
    
    public void Reduce(float amount)
    {
        _current.Value = Mathf.Max(0, _current.Value - amount);
        OnHitTaken?.Invoke();
    }

    public void Add(float amount)
    {
        _current.Value = Mathf.Min(_max.Value, _current.Value + amount);
    }
    
    private void UpdateMaxHealth(float newMaxHealth)
    {
        var difference = newMaxHealth - _max.Value;
        _max.Value = newMaxHealth;
        _current.Value = Mathf.Clamp(_current.Value + difference, 0, _max.Value);
    }
}
}