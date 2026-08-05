using _Game.Scripts.Player;
using _Game.Scripts.Player.Modules.Movement;

namespace _Game.Scripts.Abilities
{
public class SprintAbility: AbilityController
{
    private MovementModule _movement;

    public void Construct(PlayerModel model)
    {
        base.Construct(model.Endurance);
        _movement = model.Movement;
    }

    protected override void Enable()
    {
        base.Enable();
        _movement.UseSprint = true;
    }

    protected override void Disable()
    {
        base.Disable();
        _movement.UseSprint = false;
    }
}
}