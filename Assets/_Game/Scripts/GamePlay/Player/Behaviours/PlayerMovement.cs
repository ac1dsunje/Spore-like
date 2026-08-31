using _Game.Scripts.GamePlay.Entities;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.GamePlay.Player.Behaviours
{
public class PlayerMovement: IInitializable, IFixedTickable, ITickable
{
    [Inject] private RigidbodyController _controller;
    [Inject] private MovementModule _movement;
    [Inject] private DisguiseModule _disguise;
    
    public float Horizontal => (Input.GetKey(KeyCode.D) ? 1f : 0f) - (Input.GetKey(KeyCode.A) ? 1f : 0f);
    public float Vertical => (Input.GetKey(KeyCode.W) ? 1f : 0f) - (Input.GetKey(KeyCode.S) ? 1f : 0f);
    public Vector2 Movement => new(Horizontal, Vertical);

    private Vector2 _lastMovementDirection = Vector2.right;
    private Vector3Int GridPosition => _controller.GridPosition;

    public void Initialize()
    {
        _movement.SetTransform(_controller.transform);
    }

    public void Tick()
    {
        TryFlip();
    }

    public void FixedTick()
    {
        var input = Movement.normalized;

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
        if (Horizontal != 0)
        {
            _controller.Flip(Horizontal > 0);
        }
    }
}
}