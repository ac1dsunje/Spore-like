using System;
using _Game.Scripts.GamePlay.Types;
using _Game.Scripts.GamePlay.UI.Bar;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Modules
{
public class StomachModule: StatModule, IResource
{
    public float MaxHunger { get; private set; }
    public float Hunger { get; private set; }
    
    public event Action<float, float> OnValueChanged;

    public event Action<float> OnFoodPointsAchieved;

    protected override void Configure()
    {
        BindStat(StatType.MaxHunger, UpdateMaxHunger);
    }

    public void LoseHunger(float value)
    {
        Hunger -= value;
        if (Hunger <= 0) Hunger = 0;
        OnValueChanged?.Invoke(Hunger, MaxHunger);
    }

    private void UpdateMaxHunger(float value)
    {
        var difference = value - MaxHunger;
        MaxHunger = value;
        Hunger = Mathf.Clamp(Hunger +difference, 0, MaxHunger);
        
        OnValueChanged?.Invoke(Hunger, MaxHunger);
    }

    public void GetExperienceFromFood(int value)
    {
        Hunger += value;
        OnFoodPointsAchieved?.Invoke(value);
        OnValueChanged?.Invoke(Hunger, MaxHunger);
    }
}
}