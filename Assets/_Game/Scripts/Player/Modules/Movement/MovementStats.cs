namespace _Game.Scripts.Player.Modules.Movement
{
public class MovementStats
{
    public float MoveSpeed { get; private set; }
    public float Acceleration { get; private set; }
    public float Inertia { get; private set; }

    public void UpdateMoveSpeed(float newValue)
    {
        MoveSpeed = newValue;
    }

    public void UpdateAcceleration(float newValue)
    {
        Acceleration = newValue;
    }

    public void UpdateInertia(float newValue)
    {
        Inertia = newValue;
    }
}
}