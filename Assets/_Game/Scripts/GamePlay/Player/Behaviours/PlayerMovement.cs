using _Game.Scripts.GamePlay.Modules;
using _Game.Scripts.GamePlay.Movement;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Behaviours
{
public class PlayerMovement: MonoBehaviour
{
    [SerializeField] private MovementController _controller;

    private Vector3Int GridPosition => _controller.GridPosition;
    private MovementModule _movement;
    private DisguiseModule _disguise;
    private PlayerInputService _inputService;

    private Vector2 _lastMovementDirection = Vector2.right;

    [Inject]
    private void Construct(MovementModule movement, DisguiseModule disguise, PlayerInputService inputService)
    {
        _movement = movement;
        _disguise = disguise;
        _inputService = inputService;
    }

    private void Update()
    {
        TryFlip();
    }

    private void FixedUpdate()
    {
        var input = _inputService.Movement.normalized;

        UpdateLastMovementDirection(input);

        Move(input);
        TryDash();
        
        _disguise.SetMoving(_controller.IsMoving);
        _movement.UpdateGridPosition(GridPosition);
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