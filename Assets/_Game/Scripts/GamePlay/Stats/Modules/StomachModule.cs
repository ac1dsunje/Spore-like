using System;
using _Game.Scripts.GamePlay.Types;
using R3;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Modules
{
public class StomachModule : StatModule
{
    public ReadOnlyReactiveProperty<float> Max => _max;
    public ReadOnlyReactiveProperty<float> Current => _current;
    
    private readonly ReactiveProperty<float> _max = new();
    private readonly ReactiveProperty<float> _current = new();

    public event Action<float> OnFoodPointsAchieved;

    protected override void Configure()
    {
        BindStat(StatType.MaxHunger, UpdateMaxHunger);
    }

    public void LoseHunger(float value)
    {
        _current.Value -= value;
        if (_current.Value <= 0) _current.Value = 0;
    }

    private void UpdateMaxHunger(float value)
    {
        var difference = value - _max.Value;
        _max.Value = value;
        _current.Value = Mathf.Clamp(_current.Value + difference, 0, _max.Value);
    }

    public void GetExperienceFromFood(int value)
    {
        _current.Value += value;
        OnFoodPointsAchieved?.Invoke(value);
    }
}
}