using _Game.Scripts.GamePlay.Modules;

namespace _Game.Scripts.GamePlay.Abilities.Types
{
public class DashAbility: Ability
{
    private readonly MovementModule _movement;

    public DashAbility(MovementModule movement, EnduranceModule endurance, AbilityConfig config) 
        : base(endurance, config)
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