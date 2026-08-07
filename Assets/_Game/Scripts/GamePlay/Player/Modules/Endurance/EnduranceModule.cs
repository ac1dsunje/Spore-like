using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Abilities;
using _Game.Scripts.GamePlay.Player.Modules.Stats;
using _Game.Scripts.GamePlay.Stats;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Player.Modules.Endurance
{
public class EnduranceModule: StatModule
{
    private float _maxEndurance;
    public float EnduranceRecovery { get; private set; }
    
    private float _endurance;

    public bool IsUsed => _abilityControllers.Count > 0;
    
    private readonly HashSet<Ability> _abilityControllers = new();
    public event Action<float, float> OnEnduranceChanged;

    public EnduranceModule(PlayerStats playerStats) : base(playerStats)
    {
        BindStat(StatType.MaxEndurance, UpdateMaxEndurance);
        BindStat(StatType.EnduranceRecovery, UpdateEnduranceRecovery);
    }

    public bool HasEnoughEndurance(float value) => _endurance >= value;

    public void AddUser(Ability ability) => _abilityControllers.Add(ability);

    public void RemoveUser(Ability ability) => _abilityControllers.Remove(ability);

    public void AddEndurance(float value)
    {
        var endurance = _endurance;
        _endurance += value;
        if (_endurance > _maxEndurance)
        {
            _endurance = _maxEndurance;
        }
        if (Mathf.Approximately(endurance, _endurance)) return;
        OnEnduranceChanged?.Invoke(_endurance, _maxEndurance);
    }

    public void UseEndurance(float value)
    {
        var endurance = _endurance;
        _endurance -= value;
        if (_endurance <= 0) _endurance = 0;
        
        if (Mathf.Approximately(endurance, _endurance)) return;
        OnEnduranceChanged?.Invoke(_endurance, _maxEndurance);
    }
    
    private void UpdateMaxEndurance(float value)
    {
        var difference = value - _maxEndurance;
        _maxEndurance = value;
        
        _maxEndurance = value;
    
        _endurance = Mathf.Clamp(_endurance + difference, 0, _maxEndurance);
        
        OnEnduranceChanged?.Invoke(_endurance, _maxEndurance);
    }

    private void UpdateEnduranceRecovery(float value)
    {
        EnduranceRecovery = value;
    }
}
}