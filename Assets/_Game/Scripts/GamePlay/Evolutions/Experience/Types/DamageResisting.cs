using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Evolutions.Experience.Types
{
public class DamageResisting: EvolutionExperienceService
{
    public DamageResisting(PlayerModel playerModel, float amount) : base(playerModel, amount) => PlayerModel.Defense.OnDamageResisted += AddAmount;

    public override void Dispose() => PlayerModel.Defense.OnDamageResisted -= AddAmount;
}
}