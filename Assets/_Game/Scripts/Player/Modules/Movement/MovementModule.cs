using _Game.Scripts.Evolutions.Stats;

namespace _Game.Scripts.Player.Modules.Movement
{
public class MovementModule: StatModule
{
    public float MoveSpeed { get; private set; }
    public float Acceleration { get; private set; }
    public float Inertia { get; private set; }

    public MovementModule(PlayerStats stats): base(stats) {}

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
    
    private void UpdateMoveSpeed(float newValue) => MoveSpeed = newValue;

    private void UpdateAcceleration(float newValue) => Acceleration = newValue;

    private void UpdateInertia(float newValue) => Inertia = newValue;
}
}