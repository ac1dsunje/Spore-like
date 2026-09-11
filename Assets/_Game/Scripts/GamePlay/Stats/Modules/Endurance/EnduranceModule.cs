using _Game.Scripts.GamePlay.Types;
using R3;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Modules.Endurance
{
public class EnduranceModule : StatModule
{
    public ReadOnlyReactiveProperty<float> Max => _max;
    public ReadOnlyReactiveProperty<float> Current => _current;
    
    private readonly ReactiveProperty<float> _max = new();
    private readonly ReactiveProperty<float> _current = new();
    
    protected override void Configure()
    {
        BindStat(StatType.MaxEndurance, UpdateMaxEndurance);
    }
    
    public bool HasEnoughEndurance(float value) => _current.Value >= value;

    public void AddEndurance(float value)
    {
        _current.Value = Mathf.Min(_max.Value, _current.Value + value);
    }
    
    public void UseEndurance(float value)
    {
        _current.Value = Mathf.Max(0, _current.Value - value);
    }
    
    private void UpdateMaxEndurance(float value)
    {
        var difference = value - _max.Value;
        _max.Value = value;
        _current.Value = Mathf.Clamp(_current.Value + difference, 0, _max.Value);
    }
}
}