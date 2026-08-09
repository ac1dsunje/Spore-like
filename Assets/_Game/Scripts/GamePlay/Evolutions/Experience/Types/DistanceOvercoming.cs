using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Evolutions.Experience.Types
{
public class DistanceOvercoming: EvolutionExperienceService
{
    public DistanceOvercoming(PlayerModel playerModel) : base(playerModel) => PlayerModel.Movement.OnDistanceOvercome += OnDistanceOvercome;

    private void OnDistanceOvercome(int value) => RaiseEvent(value);

    public override void Dispose() => PlayerModel.Movement.OnDistanceOvercome -= OnDistanceOvercome;
}
}