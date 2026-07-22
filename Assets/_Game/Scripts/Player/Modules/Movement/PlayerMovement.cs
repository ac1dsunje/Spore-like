using UnityEngine;

namespace _Game.Scripts.Player.Modules.Movement
{
public enum MovementState
{
    Enabled, 
    Disabled
}

public class PlayerMovement: MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    private MovementState _state;
    private MovementStats _stats;
    
    [SerializeField] private Rigidbody2D _rigidbody;
    
    private float _horizontalInput;
    private float _verticalInput;

    public void Disable() => SetState(MovementState.Disabled);

    public void Enable() => SetState(MovementState.Enabled);
    
    public void Construct(MovementStats stats)
    {
        _stats = stats;
    }

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        _horizontalInput = IsInState(MovementState.Disabled)? 0: Input.GetAxisRaw("Horizontal");
        _verticalInput = IsInState(MovementState.Disabled)? 0: Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var targetVelocity = new Vector2(_horizontalInput, _verticalInput) * _stats.MoveSpeed;

        var accelerationThisFrame = _stats.Acceleration * Time.fixedDeltaTime;

        _rigidbody.linearVelocity = Vector2.MoveTowards(
            _rigidbody.linearVelocity, 
            targetVelocity, 
            accelerationThisFrame
        );
        
        Flip();
    }

    private void Flip()
    {
        if (_horizontalInput != 0)
        {
            _spriteRenderer.flipX = _horizontalInput < 0;
        }
    }

    private void SetState(MovementState state) => _state = state;
    private bool IsInState(MovementState state) => _state == state;
}
}