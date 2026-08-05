using _Game.Scripts.Player.Modules.Endurance;
using UnityEngine;

namespace _Game.Scripts.Player.Modules.Movement
{

public class PlayerMovement: MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    
    private MovementModule _movement;
    private EnduranceModule _endurance;
    
    private float _horizontalInput;
    private float _verticalInput;
    private bool _sprintInput;
    
    public void Construct(MovementModule movement, EnduranceModule endurance)
    {
        _movement = movement;
        _endurance = endurance;
    }

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
        _sprintInput = Input.GetKey(KeyCode.LeftShift);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var input = new Vector2(_horizontalInput, _verticalInput).normalized;

        var sprintMultiplier = _sprintInput
            ? _endurance.HasEndurance
                ? _movement.SprintMultiplier
                : 1f
            : 1f;

        var maxSpeed = _movement.MoveSpeed * sprintMultiplier;
        var targetVelocity = input * maxSpeed;

        var hasInput = input.sqrMagnitude > 0f;
        
        _endurance.IsUsed = hasInput & _sprintInput;

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