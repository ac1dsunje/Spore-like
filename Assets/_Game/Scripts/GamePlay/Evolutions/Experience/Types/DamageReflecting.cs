using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Evolutions.Experience.Types
{
public class DamageReflecting: EvolutionExperienceService
{
    public DamageReflecting(PlayerModel playerModel, float amount) : base(playerModel, amount) => PlayerModel.Defense.OnDamageReflected += AddAmount;

    public override void Dispose() => PlayerModel.Defense.OnDamageReflected -= AddAmount;
}
}