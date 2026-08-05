using _Game.Scripts.Player;

namespace _Game.Scripts.Abilities.Types
{
public class SprintAbility: Ability
{
    public SprintAbility(PlayerModel model, AbilityConfig config, Ticker ticker) : base(model, config, ticker) { }

    protected override void Enable()
    {
        base.Enable();
        Model.Movement.UseSprint = true;
    }

    protected override void Disable()
    {
        base.Disable();
        Model.Movement.UseSprint = false;
    }
}
}