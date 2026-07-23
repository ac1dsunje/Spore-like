using System;
using _Game.Scripts.Evolutions.Stats;
using _Game.Scripts.World.Food;

namespace _Game.Scripts.Player.Modules.Vision
{
public class VisionStats: IDisposable
{
    public float VisionRadius { get; private set; }
    public float SensoricsRadius { get; private set; }

    public event Action<float> OnVisionRadiusChanged;
    public event Action<FoodItem> OnFoodDiscovered;

    private readonly PlayerStats _stats;

    public VisionStats(PlayerStats stats)
    {
        _stats = stats;
        _stats.OnStatUpdated += OnStatUpdated;
    }

    private void OnStatUpdated(EvolutionType type, float value)
    {
        switch (type)
        {
            case EvolutionType.VisionRadius:
                UpdateRadius(value);
                break;
            case EvolutionType.SensoricsRadius:
                UpdateSensoricsRadius(value);
                break;
        }
    }
    
    private void UpdateRadius(float newRadius)
    {
        VisionRadius = newRadius;
        OnVisionRadiusChanged?.Invoke(VisionRadius);
    }

    private void UpdateSensoricsRadius(float newRadius) => SensoricsRadius = newRadius;

    public void DiscoverFood(FoodItem food)
    {
        OnFoodDiscovered?.Invoke(food);
    }

    public void Dispose()
    {
        _stats.OnStatUpdated -= OnStatUpdated;
    }
}
}