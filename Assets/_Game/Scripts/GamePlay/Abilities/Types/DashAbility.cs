using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Modules;

namespace _Game.Scripts.GamePlay.Abilities.Types
{
public class DashAbility: Ability
{
    private readonly MovementModule _movement;

    public DashAbility(MovementModule movement, EnduranceModule endurance, AbilityConfig config, 
        Ticker ticker) : base(endurance, config, ticker)
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