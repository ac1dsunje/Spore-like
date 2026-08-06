using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Evolutions.Experience.Types
{
public class DamageTaking: EvolutionExperienceService
{
    public DamageTaking(PlayerModel playerModel) : base(playerModel) => PlayerModel.Health.OnDamageTaken += OnDamageTaken;

    private void OnDamageTaken(int damage) => RaiseEvent(damage);

    public override void Dispose() => PlayerModel.Health.OnDamageTaken -= OnDamageTaken;
}
}