using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Evolutions.Experience.Types
{
public class FoodEating: EvolutionExperienceService
{
    public FoodEating(PlayerModel playerModel, float amount) : base(playerModel, amount)
    {
        PlayerModel.MouthModule.OnFoodPointsAchieved += AddAmount;
    }

    public override void Dispose() => PlayerModel.MouthModule.OnFoodPointsAchieved -= AddAmount;
}
}