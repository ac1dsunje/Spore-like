using System;
using _Game.Scripts.Player;

namespace _Game.Scripts.Evolutions.Experience.Types
{
public class FoodEating: IEvolutionExperience
{
    private readonly PlayerModel _playerModel;

    public event Action<int> OnExperienceGained;
    public FoodEating(PlayerModel playerModel)
    {
        _playerModel = playerModel;
        _playerModel.EatModule.OnFoodPointsAchieved += OnFoodEaten;
    }
    
    private void OnFoodEaten(int value)
    {
        OnExperienceGained?.Invoke(value);
    }

    public void Dispose()
    {
        _playerModel.EatModule.OnFoodPointsAchieved -= OnFoodEaten;
    }
}
}