using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Entity.Module;

namespace _Game.Scripts.GamePlay.Abilities.Types
{
public class DashAbility: Ability
{
    private readonly MovementModule _movement;

    public DashAbility(MovementModule movement, EnduranceModule endurance, AbilityConfig config, 
        Ticker ticker, IInputService input) : base(endurance, config, ticker, input)
    {
        _movement = movement;
    }

    protected override void Enable()
    {
        base.Enable();
        _movement.SetDash(true);
    }
}
}