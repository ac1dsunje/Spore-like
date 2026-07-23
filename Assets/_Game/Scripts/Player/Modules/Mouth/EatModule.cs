using System;
using _Game.Scripts.Evolutions.Stats;
using _Game.Scripts.World.Food;

namespace _Game.Scripts.Player.Modules.Mouth
{
public class EatModule: IDisposable
{
    public float EatingSpeed { get; private set; }

    public event Action<FoodItem> OnFoodEaten;
    public event Action<int> OnFoodPointsAchieved;

    private PlayerStats _stats;

    public EatModule(PlayerStats stats)
    {
        _stats = stats;
        _stats.OnStatUpdated += OnStatUpdated;
    }

    private void OnStatUpdated(EvolutionType type, float value)
    {
        switch (type)
        {
            case EvolutionType.EatingSpeed:
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

    public void Dispose()
    {
        _stats.OnStatUpdated -= OnStatUpdated;
    }
}
}