using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Movement;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Player.Behaviours
{
public class PlayerMovement: IFixedTickable, ITickable
{
    [Inject] private MovementController _controller;
    [Inject] private MovementModule _movement;
    [Inject] private DisguiseModule _disguise;
    [Inject] private PlayerInputService _inputService;

    private Vector2 _lastMovementDirection = Vector2.right;
    private Vector3Int GridPosition => _controller.GridPosition;

    public void Tick()
    {
        TryFlip();
    }

    public void FixedTick()
    {
        var input = _inputService.Movement.normalized;

        UpdateLastMovementDirection(input);

        Move(input);
        TryDash();
        
        _disguise.SetMoving(_controller.IsMoving);
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
        if (_inputService.Horizontal != 0)
        {
            _controller.Flip(_inputService.Horizontal > 0);
        }
    }
}
}