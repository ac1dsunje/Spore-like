using System;
using _Game.Scripts.GamePlay.Player.Modules.Stats;
using _Game.Scripts.GamePlay.Stats;

namespace _Game.Scripts.GamePlay.Player.Modules.Mouth
{
public class MouthModule: StatModule
{
    public float EatingStrength { get; private set; }
    public float EatingPenetration { get; private set; }
    public float EatingTime => _eatingTime / 100f;

    private float _eatingTime;
    public event Action<int> OnFoodPointsAchieved;

    public MouthModule(PlayerStats playerStats) : base(playerStats)
    {
        BindStat(StatType.EatingStrength, UpdateEatingStrength);
        BindStat(StatType.EatingPenetration, UpdateEatingPenetration);
        BindStat(StatType.EatingTime, UpdateEatingTime);
    }

    private void UpdateEatingStrength(float value) => EatingStrength = value;
    private void UpdateEatingPenetration(float value) => EatingPenetration = value;
    private void UpdateEatingTime(float value) => _eatingTime = value;

    public void GetExperienceFromFood(int value)
    {
        OnFoodPointsAchieved?.Invoke(value);
    }
}
}