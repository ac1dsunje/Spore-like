using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Player;

namespace _Game.Scripts.GamePlay.Abilities.Types
{
public class DashAbility: Ability
{
    public DashAbility(PlayerModel model, AbilityConfig config, Ticker ticker, IInputService input) 
        : base(model, config, ticker, input) { }

    protected override void Enable()
    {
        base.Enable();
        Model.Movement.SetDash(true);
    }
}
}