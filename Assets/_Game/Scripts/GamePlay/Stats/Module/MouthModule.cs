using System;

namespace _Game.Scripts.GamePlay.Module
{
public class MouthModule: StatModule
{
    public float EatingStrength { get; private set; }
    public float EatingPenetration { get; private set; }
    public float EatingTime { get; private set; }

    public event Action<float> OnFoodPointsAchieved;

    protected override void Configure()
    {
        BindStat(StatType.EatingStrength, UpdateEatingStrength);
        BindStat(StatType.EatingPenetration, UpdateEatingPenetration);
        BindStat(StatType.EatingTime, UpdateEatingTime);
    }

    private void UpdateEatingStrength(float value) => EatingStrength = value;
    private void UpdateEatingPenetration(float value) => EatingPenetration = value;
    private void UpdateEatingTime(float value) => EatingTime = value / 100f;

    public void GetExperienceFromFood(int value)
    {
        OnFoodPointsAchieved?.Invoke(value);
    }
}
}