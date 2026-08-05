using _Game.Scripts.Player.Modules.Endurance;
using UnityEngine;

namespace _Game.Scripts.Player.Modules.Movement
{

public class PlayerMovement: MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    
    private MovementModule _movement;
    
    private float _horizontalInput;
    private float _verticalInput;
    
    public void Construct(MovementModule movement)
    {
        _movement = movement;
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
    }

    private void Move()
    {
        var input = new Vector2(_horizontalInput, _verticalInput).normalized;

        if (_movement.DashRequested)
        {
            _rigidbody.AddForce(input * _movement.DashPower, ForceMode2D.Impulse);
            _movement.ResetDash();
        }
        
        var maxSpeed = _movement.MoveSpeed;
        var targetVelocity = input * maxSpeed;

        var hasInput = input.sqrMagnitude > 0f;

        var time = hasInput
            ? _movement.Acceleration
            : _movement.Inertia;

        var rate = maxSpeed / time;

        _rigidbody.linearVelocity = Vector2.MoveTowards(
            _rigidbody.linearVelocity,
            targetVelocity,
            rate * Time.fixedDeltaTime);

        Flip();
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