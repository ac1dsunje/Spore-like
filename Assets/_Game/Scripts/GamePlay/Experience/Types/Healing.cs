using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Experience.Types
{
public class Healing: ExperienceService
{
    public Healing(PlayerModel playerModel, float amount) : base(playerModel, amount)
    {
        PlayerModel.Health.OnHealed += AddAmount;
    }

    public override void Dispose() => PlayerModel.Health.OnHealed -= AddAmount;
}
}