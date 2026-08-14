using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Experience.Types
{
public class EnduranceRecovering: ExperienceService
{
    public EnduranceRecovering(PlayerModel playerModel, float amount) : base(playerModel, amount)
    {
        PlayerModel.Endurance.OnEnduranceRecovered += AddAmount;
    }

    public override void Dispose() => PlayerModel.Endurance.OnEnduranceRecovered -= AddAmount;
}
}