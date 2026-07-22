using UnityEngine;

namespace _Game.Scripts.Player.Modules
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
    private PlayerStats _stats;
    
    [SerializeField] private Rigidbody2D _rigidbody;
    
    private float _horizontalVelocity;
    private float _verticalVelocity;

    public void Disable() => SetState(MovementState.Disabled);

    public void Enable() => SetState(MovementState.Enabled);
    
    public void Construct(PlayerStats stats)
    {
        _stats = stats;
    }

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        _horizontalVelocity = IsInState(MovementState.Disabled)? 0: Input.GetAxis("Horizontal");
        _verticalVelocity = IsInState(MovementState.Disabled)? 0: Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rigidbody.linearVelocity = new Vector2(_horizontalVelocity * _stats.MoveSpeed, _verticalVelocity * _stats.MoveSpeed);
        Flip();
    }

    private void Flip()
    {
        if (_rigidbody.linearVelocity.x != 0)
        {
            _spriteRenderer.flipX = _rigidbody.linearVelocity.x < 0;
        }
    }

    private void SetState(MovementState state) => _state = state;
    private bool IsInState(MovementState state) => _state == state;
}
}