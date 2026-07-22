using System.Collections.Generic;
using _Game.Scripts.Player;
using _Game.Scripts.World.Food;

namespace _Game.Scripts.Evolutions.Types.Vision.CameraEyes
{
public class CameraEyes: Evolution
{
    private readonly List<FoodItem> _discoveredFood = new();
    
    public CameraEyes(EvolutionConfig config) : base(config) {}

    public override void Apply(PlayerStats playerStats)
    {
        base.Apply(playerStats);
        Player.OnFoodDiscovered += OnFoodDiscovered;
        Player.OnFoodEaten += OnFoodEaten;
    }

    private void OnFoodDiscovered(FoodItem food)
    {
        if (_discoveredFood.Contains(food)) return;
        
        _discoveredFood.Add(food);
        UpdateExperience(food.FeedAmount);
    }

    private void OnFoodEaten(FoodItem food)
    {
        if (_discoveredFood.Contains(food))
        {
            _discoveredFood.Remove(food);
        }
    }

    public override void Dispose()
    {
        if (Player == null) return;
            
        Player.OnFoodDiscovered -= OnFoodDiscovered;
        Player.OnFoodEaten += OnFoodEaten;
    }
}
}