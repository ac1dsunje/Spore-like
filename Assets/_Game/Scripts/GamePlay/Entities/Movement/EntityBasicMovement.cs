using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities.Movement
{
public class EntityBasicMovement: IFixedTickable, ITickable, IMovementController
{
    private readonly RigidbodyController _controller;
    private readonly MovementModule _movement;

    private Vector2 _lastMovementDirection = Vector2.right;
    private Vector3Int GridPosition => _controller.GridPosition;

    private Vector2 _direction;

    public EntityBasicMovement(RigidbodyController controller, MovementModule movement)
    {
        _controller = controller;
        _movement = movement;
    }
    
    public void SetDirection(Vector2 direction) => _direction = direction;

    public void Tick()
    {
        TryFlip();
    }

    public void FixedTick()
    {
        var input = _direction.normalized;

        UpdateLastMovementDirection(input);

        Move(input);
        TryDash();
        _movement.UpdateGridPosition(GridPosition);
        
        _controller.SetMaterial(_movement.Friction, _movement.Bounciness);
    }

    private void UpdateLastMovementDirection(Vector2 input)
    {
        if (input.sqrMagnitude > 0f)
        {
            _lastMovementDirection = input;
        }
    }

    private void Move(Vector2 input)
    {
        var targetVelocity = input * _movement.MoveSpeed;

        var hasInput = input.sqrMagnitude > 0f;

        var time = hasInput ? _movement.Acceleration : _movement.Inertia;

        var rate = _movement.MoveSpeed / time;
        
        _controller.Move(targetVelocity, rate * Time.fixedDeltaTime);
    }

    private void TryDash()
    {
        if (!_movement.DashRequested) return;

        _controller.Push(_lastMovementDirection, _movement.DashPower);

        _movement.SetDash(false);
    }

    private void TryFlip()
    {
        if (_direction.x != 0)
        {
            _controller.Flip(_direction.x > 0);
        }
    }
}
}