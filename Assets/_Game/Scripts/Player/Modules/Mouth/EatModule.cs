using System;
using _Game.Scripts.Evolutions.Stats;
using _Game.Scripts.Player.Modules.Stats;

namespace _Game.Scripts.Player.Modules.Mouth
{
public class EatModule: StatModule
{
    public float EatingStrength { get; private set; }
    public float EatingPenetration { get; private set; }
    public float EatingTime { get; private set; }
    public event Action<int> OnFoodPointsAchieved;

    public EatModule(PlayerStats playerStats): base(playerStats) {}

    protected override void PlayerStatUpdated(StatType type, float value)
    {
        switch (type)
        {
            case StatType.EatingStrength:
                UpdateEatingStrength(value);
                break;
            
            case StatType.EatingPenetration:
                UpdateEatingPenetration(value);
                break;
            
            case StatType.EatingTime:
                UpdateEatingTime(value);
                break;
        }
    }

    private void UpdateEatingStrength(float newValue) => EatingStrength = newValue;
    private void UpdateEatingPenetration(float newValue) => EatingPenetration = newValue;
    private void UpdateEatingTime(float newValue) => EatingTime = newValue;

    public void GetExperienceFromFood(int value)
    {
        OnFoodPointsAchieved?.Invoke(value);
    }
}
}