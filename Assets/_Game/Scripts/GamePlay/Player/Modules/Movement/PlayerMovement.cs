using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Player.Modules.Movement
{
public class PlayerMovement: MonoBehaviour
{
    [SerializeField] private MovementController _controller;

    private Vector3Int GridPosition => _controller.GridPosition;
    private MovementModule _module;
    private PlayerInputService _inputService;
    private PlayerAuthority _authority;

    [Inject]
    private void Construct(MovementModule movement, PlayerInputService inputService, PlayerAuthority authority)
    {
        _module = movement;
        _inputService = inputService;
        _authority = authority;
    }

    private void Update()
    {
        TryFlip();
    }

    private void FixedUpdate()
    {
        if (_authority.IsLocal)
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
        if (_inputService.Horizontal != 0 && _authority.IsLocal)
        {
            _controller.Flip(_inputService.Horizontal > 0);
        }
    }
}
}