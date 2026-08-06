using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Evolutions.Experience.Types
{
public class DamageResisting: EvolutionExperienceService
{
    public DamageResisting(PlayerModel playerModel) : base(playerModel) => PlayerModel.Defense.OnDamageResisted += OnDamageResisted;

    private void OnDamageResisted(int damage) => RaiseEvent(damage);

    public override void Dispose() => PlayerModel.Defense.OnDamageResisted -= OnDamageResisted;
}
}