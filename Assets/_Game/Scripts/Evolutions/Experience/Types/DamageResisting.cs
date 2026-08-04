using _Game.Scripts.Player;

namespace _Game.Scripts.Evolutions.Experience.Types
{
public class DamageResisting: EvolutionExperienceService
{
    public DamageResisting(PlayerModel playerModel) : base(playerModel) => PlayerModel.Defense.OnDamageResisted += OnDamageResisted;

    private void OnDamageResisted(int damage) => RaiseEvent(damage);

    public override void Dispose() => PlayerModel.Defense.OnDamageResisted -= OnDamageResisted;
}
}