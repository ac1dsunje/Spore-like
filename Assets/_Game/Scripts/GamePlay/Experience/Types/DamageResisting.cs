using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Experience.Types
{
public class DamageResisting: ExperienceService
{
    public DamageResisting(PlayerModel playerModel, float amount) : base(playerModel, amount)
    {
        PlayerModel.Defense.OnDamageResisted += AddAmount;
    }

    public override void Dispose() => PlayerModel.Defense.OnDamageResisted -= AddAmount;
}
}