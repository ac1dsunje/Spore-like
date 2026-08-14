using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Evolutions.Experience.Types
{
public class DistanceOvercoming: EvolutionExperienceService
{
    public DistanceOvercoming(PlayerModel playerModel, float amount) : base(playerModel, amount)
    {
        PlayerModel.Movement.OnDistanceOvercome += AddAmount;
    }

    public override void Dispose() => PlayerModel.Movement.OnDistanceOvercome -= AddAmount;
}
}