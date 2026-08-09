using System;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Player.Modules.Movement
{

public class PlayerMovement: MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    
    private MovementModule _movement;
    
    private float _horizontalInput;
    private float _verticalInput;

    private Vector3Int _lastPosition;

    public event Action OnGridPositionChanged;
    
    public void Construct(MovementModule movement)
    {
        _movement = movement;
    }    
    
    public Vector3Int GetGridPosition() => 
        new(
            (int)transform.position.x, 
            (int)transform.position.y, 
            (int)transform.position.z
        );
    
    private void CheckMoveByGrid()
    {
        var currentPos = GetGridPosition();
        if (currentPos == _lastPosition) return;
        _lastPosition = currentPos;
        OnGridPositionChanged?.Invoke();
    }

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
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