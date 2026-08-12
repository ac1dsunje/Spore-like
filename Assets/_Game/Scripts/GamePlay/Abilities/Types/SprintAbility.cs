using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Abilities.Types
{
public class SprintAbility: Ability
{
    public SprintAbility(PlayerModel model, AbilityConfig config, Ticker ticker, IInputService input)
        : base(model, config, ticker, input) { }

    protected override void Enable()
    {
        base.Enable();
        Model.Movement.RequestSprint();
    }

    protected override void Disable()
    {
        base.Disable();
        Model.Movement.ResetSprint();
    }
}
}