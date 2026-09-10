using System;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Types;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Modules
{
public class StomachModule : StatModule, IStatWithLimit
{
    private float _maxHunger;
    private float _hunger;
    
    public event Action<float, float> OnValueChanged;

    public event Action<float> OnFoodPointsAchieved;

    protected override void Configure()
    {
        BindStat(StatType.MaxHunger, UpdateMaxHunger);
    }

    public void LoseHunger(float value)
    {
        _hunger -= value;
        if (_hunger <= 0) _hunger = 0;
        OnValueChanged?.Invoke(_hunger, _maxHunger);
    }

    private void UpdateMaxHunger(float value)
    {
        var difference = value - _maxHunger;
        _maxHunger = value;
        _hunger = Mathf.Clamp(_hunger +difference, 0, _maxHunger);
        
        OnValueChanged?.Invoke(_hunger, _maxHunger);
    }

    public void GetExperienceFromFood(int value)
    {
        _hunger += value;
        OnFoodPointsAchieved?.Invoke(value);
        OnValueChanged?.Invoke(_hunger, _maxHunger);
    }
}
}