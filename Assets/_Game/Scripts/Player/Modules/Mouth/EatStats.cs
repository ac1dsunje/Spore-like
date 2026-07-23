using System;
using _Game.Scripts.World.Food;

namespace _Game.Scripts.Player.Modules.Mouth
{
public class EatStats
{
    public float EatingSpeed { get; private set; }

    public event Action<FoodItem> OnFoodEaten;
    public event Action<int> OnFoodPointsAchieved;

    public void UpdateEatingSpeed(float eatingSpeed)
    {
        EatingSpeed = eatingSpeed;
    }

    public void EatFood(FoodItem food)
    {
        OnFoodEaten?.Invoke(food);
        OnFoodPointsAchieved?.Invoke(food.FeedAmount);

        food.Release();
    }
}
}