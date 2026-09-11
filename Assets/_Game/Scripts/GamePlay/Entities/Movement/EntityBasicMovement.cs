using System;
using _Game.Scripts.Core.Services;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Entities.Movement
{
public class EntityBasicMovement : IStartable, IMovementController, IDisposable
{
    private readonly RigidbodyController _controller;
    private readonly MovementModule _movement;
    private readonly Ticker _ticker;

    private Vector2 _lastMovementDirection = Vector2.right;
    private Vector3Int GridPosition => _controller.GridPosition;

    private Vector2 _direction;

    public EntityBasicMovement(RigidbodyController controller, MovementModule movement, Ticker ticker)
    {
        _controller = controller;
        _movement = movement;
        _ticker = ticker;
    }
    
    public void Start()
    {
        _ticker.OnFixedTick += FixedTick;
    }
    
    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;
        TryFlip(direction);
        UpdateLastMovementDirection(_direction);
    }

    private void TryFlip(Vector2 direction)
    {
        if (direction.x != 0)
        {
            _controller.Flip(direction.x > 0);
        }
    }

    public void FixedTick()
    {
        Move(_direction);
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

    public void Dispose()
    {
        _ticker.OnFixedTick -= FixedTick;
    }
}
}