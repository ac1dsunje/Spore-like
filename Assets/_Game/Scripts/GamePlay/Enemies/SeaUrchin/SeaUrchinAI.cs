using _Game.Scripts.GamePlay.Entities.Movement;
using UnityEngine;
using VContainer;
using VContainer.Unity;
namespace _Game.Scripts.GamePlay.Enemies.SeaUrchin
{
public class SeaUrchinAI : ITickable
{
    private IMovementController _movement;

    private float _directionChangeTimer;

    private const float MinDirectionChangeTime = 0.5f;
    private const float MaxDirectionChangeTime = 2f;

    [Inject]
    private void Construct(IMovementController movement)
    {
        _movement = movement;
    }

    public void Tick()
    {
        _directionChangeTimer -= Time.deltaTime;

        if (_directionChangeTimer > 0f)
            return;

        ChangeDirection();
    }

    private void ChangeDirection()
    {
        var direction = Random.insideUnitCircle.normalized;

        if (direction.sqrMagnitude <= Mathf.Epsilon) direction = Vector2.right;

        _movement.SetDirection(direction);

        _directionChangeTimer = Random.Range(MinDirectionChangeTime, MaxDirectionChangeTime);
    }
}
}