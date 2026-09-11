using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Types;
using R3;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Modules
{
public class EnduranceModule : StatModule
{
    public float EnduranceRecovery { get; private set; }
    public ReadOnlyReactiveProperty<float> Max => _max;
    public ReadOnlyReactiveProperty<float> Current => _current;
    
    private readonly ReactiveProperty<float> _max = new();
    private readonly ReactiveProperty<float> _current = new();
    public bool IsUsed => _users.Count > 0;
    
    private readonly HashSet<IEnduranceUser> _users = new();
    public event Action<float> OnEnduranceRecovered;

    protected override void Configure()
    {
        BindStat(StatType.MaxEndurance, UpdateMaxEndurance);
        BindStat(StatType.EnduranceRecovery, UpdateEnduranceRecovery);
    }

    public bool HasEnoughEndurance(float value) => _current.Value >= value;

    public void AddUser(IEnduranceUser user) => _users.Add(user);

    public void RemoveUser(IEnduranceUser user) => _users.Remove(user);

    public void AddEndurance(float value)
    {
        var endurance = _current.Value;
        _current.Value += value;
        if (_current.Value > _max.Value)
        {
            _current.Value = _max.Value;
        }
        if (Mathf.Approximately(endurance, _current.Value)) return;
        OnEnduranceRecovered?.Invoke(value);
    }

    public void UseEndurance(float value)
    {
        var endurance = _current.Value;
        _current.Value -= value;
        if (_current.Value <= 0) _current.Value = 0;
    }
    
    private void UpdateMaxEndurance(float value)
    {
        var difference = value - _max.Value;
        _max.Value = value;
    
        _current.Value = Mathf.Clamp(_current.Value + difference, 0, _max.Value);
    }

    private void UpdateEnduranceRecovery(float value) => EnduranceRecovery = value;
}
}