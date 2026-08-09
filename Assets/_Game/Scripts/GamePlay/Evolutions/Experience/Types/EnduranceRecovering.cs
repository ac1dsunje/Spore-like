using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Evolutions.Experience.Types
{
public class EnduranceRecovering: EvolutionExperienceService
{
    public EnduranceRecovering(PlayerModel playerModel) : base(playerModel) => PlayerModel.Endurance.OnEnduranceRecovered += OnEnduranceRecovered;

    private void OnEnduranceRecovered(int value) => RaiseEvent(value);

    public override void Dispose() => PlayerModel.Endurance.OnEnduranceRecovered -= OnEnduranceRecovered;
}
}