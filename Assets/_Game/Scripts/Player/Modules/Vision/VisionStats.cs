using System;
using _Game.Scripts.World.Food;

namespace _Game.Scripts.Player.Modules.Vision
{
public class VisionStats
{
    public float VisionRadius { get; private set; }
    public float SensoricsRadius { get; private set; }

    public event Action<float> OnVisionRadiusChanged;
    public event Action<FoodItem> OnFoodDiscovered;

    public void UpdateRadius(float newRadius)
    {
        VisionRadius = newRadius;
        OnVisionRadiusChanged?.Invoke(VisionRadius);
    }

    public void UpdateSensoricsRadius(float newRadius)
    {
        SensoricsRadius = newRadius;
    }

    public void DiscoverFood(FoodItem food)
    {
        OnFoodDiscovered?.Invoke(food);
    }
}
}