using System;
using _Game.Scripts.Evolutions.Stats;
using _Game.Scripts.Player.Modules.Stats;
using _Game.Scripts.World.Food;

namespace _Game.Scripts.Player.Modules.Mouth
{
public class EatModule: StatModule
{
    public float EatingSpeed { get; private set; }

    public event Action<FoodItem> OnFoodEaten;
    public event Action<int> OnFoodPointsAchieved;

    public EatModule(PlayerStatsModule playerStatsModule): base(playerStatsModule) {}

    protected override void PlayerStatModuleUpdated(StatType type, float value)
    {
        switch (type)
        {
            case StatType.EatingSpeed:
                UpdateEatingSpeed(value);
                break;
        }
    }

    private void UpdateEatingSpeed(float eatingSpeed) => EatingSpeed = eatingSpeed;

    public void EatFood(FoodItem food)
    {
        OnFoodEaten?.Invoke(food);
        OnFoodPointsAchieved?.Invoke(food.FeedAmount);

        food.Release();
    }
}
}