using _Game.Scripts.Evolutions.Stats;

namespace _Game.Scripts.Player.Modules.Movement
{
public enum MovementState
{
    Enabled,
    Disabled,
}
public class MovementModule: StatModule
{
    public float MoveSpeed => _state == MovementState.Enabled? _moveSpeed : 0;
    public float Acceleration => _acceleration / 100f;
    public float Inertia => _inertia / 100f;
    
    private float _moveSpeed;
    private float _acceleration;
    private float _inertia;

    private MovementState _state;

    public MovementModule(PlayerStats stats): base(stats) {}
    
    public void Disable() => SetState(MovementState.Disabled);

    public void Enable() => SetState(MovementState.Enabled);

    protected override void OnStatUpdated(StatType type, float value)
    {
        switch (type)
        {
            case StatType.MoveSpeed:
                UpdateMoveSpeed(value);
                break;

            case StatType.Acceleration:
                UpdateAcceleration(value);
                break;

            case StatType.Inertia:
                UpdateInertia(value);
                break;
        }
    }
    
    private void UpdateMoveSpeed(float newValue) => _moveSpeed = newValue;

    private void UpdateAcceleration(float newValue) => _acceleration = newValue;

    private void UpdateInertia(float newValue) => _inertia = newValue;

    private void SetState(MovementState newState) => _state = newState;
}
}