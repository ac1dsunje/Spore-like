using System;
using _Game.Scripts.Core.Services;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Player.Modules.Movement
{

public class PlayerMovement: MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    
    public Vector3Int GridPosition => 
        new(
            (int)transform.position.x, 
            (int)transform.position.y, 
            (int)transform.position.z
        );
    
    private MovementModule _movement;
    private IInputService _inputService;
    
    private float _horizontalInput;
    private float _verticalInput;

    private Vector3Int _lastPosition;

    public event Action<PlayerMovement> OnGridPositionChanged;
    
    public void Construct(MovementModule movement, IInputService inputService)
    {
        _movement = movement;
        _inputService = inputService;
    }    
    
    private void CheckMoveByGrid()
    {
        var currentPos = GridPosition;
        if (currentPos == _lastPosition) return;
        _lastPosition = currentPos;
        OnGridPositionChanged?.Invoke(this);
        _movement.OvercomeDistance();
    }

    private void Update()
    {
        ReadInput();
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
        Move();
        CheckMoveByGrid();
    }

    private void Move()
    {
        var input = new Vector2(_horizontalInput, _verticalInput).normalized;

        TryDash(input);
        
        var targetVelocity = input * _movement.MoveSpeed;

        var hasInput = input.sqrMagnitude > 0f && _movement.CanMove;

        var time = hasInput ? _movement.Acceleration : _movement.Inertia;

        var rate = _movement.MoveSpeed / time;

        _rigidbody.linearVelocity = Vector2.MoveTowards(
            _rigidbody.linearVelocity,
            targetVelocity,
            rate * Time.fixedDeltaTime);

        Flip();
    }

    private void TryDash(Vector2 input)
    {
        if (!_movement.DashRequested) return;
        _rigidbody.AddForce(input * _movement.DashPower, ForceMode2D.Impulse);
        _movement.ResetDash();
    }

    private void Flip()
    {
        if (_horizontalInput != 0)
        {
            transform.localScale = new Vector3(_horizontalInput < 0 ? -1 : 1, 1, 1);
        }
    }
}
}