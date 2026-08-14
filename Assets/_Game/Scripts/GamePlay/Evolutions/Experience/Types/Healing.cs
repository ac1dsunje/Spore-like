using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Evolutions.Experience.Types
{
public class Healing: EvolutionExperienceService
{
    public Healing(PlayerModel playerModel, float amount) : base(playerModel, amount)
    {
        PlayerModel.Health.OnHealed += AddAmount;
    }

    public override void Dispose() => PlayerModel.Health.OnHealed -= AddAmount;
}
}