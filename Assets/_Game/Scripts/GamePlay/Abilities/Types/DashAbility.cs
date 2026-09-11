using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Modules.Endurance;

namespace _Game.Scripts.GamePlay.Abilities.Types
{
public class DashAbility : Ability
{
    private readonly MovementModule _movement;

    public DashAbility(MovementModule movement, EnduranceModule endurance, EnduranceRecoveryModule recovery, AbilityConfig config) 
        : base(endurance, recovery, config)
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