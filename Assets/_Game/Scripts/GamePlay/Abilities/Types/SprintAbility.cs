using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Modules.Endurance;

namespace _Game.Scripts.GamePlay.Abilities.Types
{
public class SprintAbility : Ability
{
    private readonly MovementModule _movement;

    public SprintAbility(MovementModule movement, EnduranceModule endurance, EnduranceRecoveryModule recovery, AbilityConfig config)
        : base(endurance, recovery, config)
    {
        _movement = movement;
    }

    protected override void Enable()
    {
        base.Enable();
        _movement.SetSprint(true);
    }

    protected override void Disable()
    {
        base.Disable();
        _movement.SetSprint(false);
    }
}
}