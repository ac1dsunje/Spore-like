using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Types;
using _Game.Scripts.GamePlay.UI.Bar;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Modules
{
public class EnduranceModule: StatModule, IResource
{
    private float _maxEndurance;
    public float EnduranceRecovery { get; private set; }
    
    private float _endurance;

    public bool IsUsed => _users.Count > 0;
    
    private readonly HashSet<IEnduranceUser> _users = new();
    public event Action<float, float> OnValueChanged;
    public event Action<float> OnEnduranceRecovered;

    protected override void Configure()
    {
        BindStat(StatType.MaxEndurance, UpdateMaxEndurance);
        BindStat(StatType.EnduranceRecovery, UpdateEnduranceRecovery);
    }

    public bool HasEnoughEndurance(float value) => _endurance >= value;

    public void AddUser(IEnduranceUser user) => _users.Add(user);

    public void RemoveUser(IEnduranceUser user) => _users.Remove(user);

    public void AddEndurance(float value)
    {
        var endurance = _endurance;
        _endurance += value;
        if (_endurance > _maxEndurance)
        {
            _endurance = _maxEndurance;
        }
        if (Mathf.Approximately(endurance, _endurance)) return;
        OnValueChanged?.Invoke(_endurance, _maxEndurance);
        OnEnduranceRecovered?.Invoke(value);
    }

    public void UseEndurance(float value)
    {
        var endurance = _endurance;
        _endurance -= value;
        if (_endurance <= 0) _endurance = 0;
        
        if (Mathf.Approximately(endurance, _endurance)) return;
        OnValueChanged?.Invoke(_endurance, _maxEndurance);
    }
    
    private void UpdateMaxEndurance(float value)
    {
        var difference = value - _maxEndurance;
        _maxEndurance = value;
    
        _endurance = Mathf.Clamp(_endurance + difference, 0, _maxEndurance);
        
        OnValueChanged?.Invoke(_endurance, _maxEndurance);
    }

    private void UpdateEnduranceRecovery(float value) => EnduranceRecovery = value;
}
}