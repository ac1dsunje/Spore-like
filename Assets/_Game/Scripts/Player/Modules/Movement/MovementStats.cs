using System;
using _Game.Scripts.Evolutions.Stats;

namespace _Game.Scripts.Player.Modules.Movement
{
public class MovementStats: IDisposable
{
    public float MoveSpeed { get; private set; }
    public float Acceleration { get; private set; }
    public float Inertia { get; private set; }
    public float Stamina { get; private set; }

    private readonly PlayerStats _stats;

    public MovementStats(PlayerStats stats)
    {
        _stats = stats;
        _stats.OnStatUpdated += OnStatUpdated;
    }

    private void OnStatUpdated(EvolutionType type, float value)
    {
        switch (type)
        {
            case EvolutionType.MoveSpeed:
                UpdateMoveSpeed(value);
                break;

            case EvolutionType.Acceleration:
                UpdateAcceleration(value);
                break;

            case EvolutionType.Inertia:
                UpdateInertia(value);
                break;
            case EvolutionType.Stamina:
                UpdateStamina(value);
                break;
        }
    }
    
    private void UpdateMoveSpeed(float newValue) => MoveSpeed = newValue;

    private void UpdateAcceleration(float newValue) => Acceleration = newValue;

    private void UpdateInertia(float newValue) => Inertia = newValue;
    private void UpdateStamina(float newValue) => Stamina = newValue;

    public void Dispose()
    {
        _stats.OnStatUpdated -= OnStatUpdated;
    }
}
}