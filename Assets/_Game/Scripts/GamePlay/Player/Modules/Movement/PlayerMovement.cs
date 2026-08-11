using System;
using _Game.Scripts.Core.Services;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Player.Modules.Movement
{

public class PlayerMovement: MonoBehaviour
{
    [SerializeField] private MovementController _controller;

    public Vector3Int GridPosition => _controller.GridPosition;
    
    private MovementModule _module;
    private IInputService _inputService;
    
    private float _horizontalInput;
    private float _verticalInput;

    private Vector3Int _lastPosition;

    public event Action<PlayerMovement> OnGridPositionChanged;
    
    public void Construct(MovementModule movement, IInputService inputService)
    {
        _module = movement;
        _inputService = inputService;
        CheckMoveByGrid();
    }    
    
    private void CheckMoveByGrid()
    {
        var currentPos = GridPosition;
        if (currentPos == _lastPosition) return;
        _lastPosition = currentPos;
        OnGridPositionChanged?.Invoke(this);
        _module.OvercomeDistance();
    }

    private void Update()
    {
        ReadInput();
        TryFlip();
    }

    private void ReadInput()
    {
        _horizontalInput = 0f;
        _verticalInput = 0f;

        if (_inputService.IsKeyPressed(KeyCode.A)) _horizontalInput -= 1f;

        if (_inputService.IsKeyPressed(KeyCode.D)) _horizontalInput += 1f;

        if (_inputService.IsKeyPressed(KeyCode.S)) _verticalInput -= 1f;

        if (_inputService.IsKeyPressed(KeyCode.W)) _verticalInput += 1f;
    }

    private void FixedUpdate()
    {
        var input = new Vector2(_horizontalInput, _verticalInput).normalized;
        Move(input);
        TryDash(input);
        CheckMoveByGrid();
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
        if (_horizontalInput != 0)
        {
            _controller.Flip(_horizontalInput > 0);
        }
    }
}
}