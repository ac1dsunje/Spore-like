using _Game.Scripts.GamePlay.Player.Modules.Stats;
using _Game.Scripts.GamePlay.Stats;

namespace _Game.Scripts.GamePlay.Player.Modules.Movement
{
public enum MovementState
{
    Enabled,
    Disabled,
}
public class MovementModule: StatModule
{
    public float MoveSpeed => _state == MovementState.Enabled? UseSprint? _moveSpeed * _sprintMultiplier : _moveSpeed : 0;
    public float Acceleration => _acceleration / 100f;
    public float Inertia => _inertia / 100f;
    public float DashPower { get; private set; }
    
    public bool UseSprint;
    public bool DashRequested { get; private set; }
    
    private float _moveSpeed;
    private float _acceleration;
    private float _inertia;
    
    private float _sprintMultiplier;

    private MovementState _state;

    public MovementModule(PlayerStats playerStats): base(playerStats) {}
    
    public void RequestDash() => DashRequested = true;
    public void ResetDash() => DashRequested = false;
    
    public void Disable() => SetState(MovementState.Disabled);

    public void Enable() => SetState(MovementState.Enabled);

    protected override void PlayerStatUpdated(StatType type, float value)
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
            
            case StatType.SprintMultiplier:
                UpdateSprintMultiplier(value);
                break;
            
            case StatType.DashPower:
                UpdateDashPower(value);
                break;
                
        }
    }
    
    private void UpdateMoveSpeed(float newValue) => _moveSpeed = newValue;

    private void UpdateAcceleration(float newValue) => _acceleration = newValue;

    private void UpdateInertia(float newValue) => _inertia = newValue;
    
    private void  UpdateSprintMultiplier(float newValue)  => _sprintMultiplier = newValue;
    
    private void UpdateDashPower(float newValue) => DashPower = newValue;

    private void SetState(MovementState newState) => _state = newState;
}
}