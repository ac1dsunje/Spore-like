using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Evolutions.Experience.Types
{
public class Healing: EvolutionExperienceService
{
    public Healing(PlayerModel playerModel) : base(playerModel) => PlayerModel.Health.OnHealed += OnHealed;

    private void OnHealed(int damage) => RaiseEvent(damage);

    public override void Dispose() => PlayerModel.Health.OnHealed -= OnHealed;
}
}