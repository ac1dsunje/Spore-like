using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Evolutions.Experience.Types
{
public class DamageReflecting: EvolutionExperienceService
{
    public DamageReflecting(PlayerModel playerModel) : base(playerModel) => PlayerModel.Defense.OnDamageReflected += OnDamageReflected;

    private void OnDamageReflected(int damage) => RaiseEvent(damage);

    public override void Dispose() => PlayerModel.Defense.OnDamageReflected -= OnDamageReflected;
}
}