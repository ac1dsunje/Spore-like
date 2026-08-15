using _Game.Scripts.GamePlay.Module;
using _Game.Scripts.GamePlay.Movement;
using _Game.Scripts.GamePlay.Network;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Behaviours
{
public class PlayerMovement: EntityNetworkBehaviour
{
    [SerializeField] private MovementController _controller;

    private Vector3Int GridPosition => _controller.GridPosition;
    private MovementModule _module;
    private PlayerInputService _inputService;

    [Inject]
    private void Construct(MovementModule movement, PlayerInputService inputService)
    {
        _module = movement;
        _inputService = inputService;
    }

    private void Update()
    {
        TryFlip();
    }

    private void FixedUpdate()
    {
        if (IsLocal)
        {
            var input = _inputService.Movement.normalized;
            Move(input);
            TryDash(input);
        }
        _module.UpdateGridPosition(GridPosition);
    }

    private void Move(Vector2 input)
    {
        var targetVelocity = input * _module.MoveSpeed;

        var hasInput = input.sqrMagnitude > 0f && _module.CanMove;

        var time = hasInput ? _module.Acceleration : _module.Inertia;

        var rate = _module.MoveSpeed / time;
        
        _controller.Move(targetVelocity, rate * Time.fixedDeltaTime);
        
    }

    private void TryDash(Vector2 input)
    {
        if (!_module.DashRequested) return;
        _controller.Push(input, _module.DashPower);
        _module.ResetDash();
    }

    private void TryFlip()
    {
        if (_inputService.Horizontal != 0 && IsLocal)
        {
            _controller.Flip(_inputService.Horizontal > 0);
        }
    }
}
}