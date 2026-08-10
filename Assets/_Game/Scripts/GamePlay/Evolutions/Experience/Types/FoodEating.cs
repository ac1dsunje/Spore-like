using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Evolutions.Experience.Types
{
public class FoodEating: EvolutionExperienceService
{
    public FoodEating(PlayerModel playerModel) : base(playerModel) => PlayerModel.MouthModule.OnFoodPointsAchieved += OnFoodEaten;

    private void OnFoodEaten(int value) => RaiseEvent(value);

    public override void Dispose() => PlayerModel.MouthModule.OnFoodPointsAchieved -= OnFoodEaten;
}
}