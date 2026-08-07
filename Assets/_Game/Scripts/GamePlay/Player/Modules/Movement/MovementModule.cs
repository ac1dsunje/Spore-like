using _Game.Scripts.GamePlay.Player.Modules.Stats;
using _Game.Scripts.GamePlay.Stats;

namespace _Game.Scripts.GamePlay.Player.Modules.Movement
{
public enum MovementState
{
    Enabled,
    Disabled
}

public class MovementModule : StatModule
{
    public float MoveSpeed => UseSprint 
        ? _moveSpeed * _sprintMultiplier 
        : _moveSpeed;

    public bool CanMove => _state == MovementState.Enabled;

    public float Acceleration => _acceleration / 100f;
    public float Inertia => _inertia / 100f;
    public float DashPower { get; private set; }
    public bool UseSprint { get; set; }
    public bool DashRequested { get; private set; }
    
    private float _moveSpeed;
    private float _acceleration;
    private float _inertia;
    private float _sprintMultiplier;

    private MovementState _state = MovementState.Enabled;
    
    public MovementModule(PlayerStats playerStats) : base(playerStats)
    {
        BindStat(StatType.MoveSpeed, UpdateMoveSpeed);
        BindStat(StatType.Acceleration, UpdateAcceleration);
        BindStat(StatType.Inertia, UpdateInertia);
        BindStat(StatType.SprintMultiplier, UpdateSprintMultiplier);
        BindStat(StatType.DashPower, UpdateDashPower);
    }
    
    public void RequestDash() => DashRequested = true;

    public void ResetDash() => DashRequested = false;

    public void Disable() => SetState(MovementState.Disabled);

    public void Enable() => SetState(MovementState.Enabled);

    private void UpdateMoveSpeed(float value) => _moveSpeed = value;

    private void UpdateAcceleration(float value) => _acceleration = value;

    private void UpdateInertia(float value) => _inertia = value;

    private void UpdateSprintMultiplier(float value) => _sprintMultiplier = value;

    private void UpdateDashPower(float value) => DashPower = value;
    
    private void SetState(MovementState state) => _state = state;
}
}