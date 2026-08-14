using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Experience.Types
{
public class DamageTaking: ExperienceService
{
    public DamageTaking(PlayerModel playerModel, float amount) : base(playerModel, amount)
    {
        PlayerModel.Health.OnDamageTaken += AddAmount;
    }

    public override void Dispose() => PlayerModel.Health.OnDamageTaken -= AddAmount;
}
}