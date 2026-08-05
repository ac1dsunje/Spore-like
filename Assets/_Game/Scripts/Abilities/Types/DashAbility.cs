using _Game.Scripts.Player;

namespace _Game.Scripts.Abilities.Types
{
public class DashAbility: Ability
{
    public DashAbility(PlayerModel model, AbilityConfig config, Ticker ticker) : base(model, config, ticker) { }

    protected override void Enable()
    {
        base.Enable();
        Model.Movement.RequestDash();
    }
}
}