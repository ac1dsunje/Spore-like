using UnityEngine;

namespace _Game.Scripts.Player.Modules.Movement
{

public class PlayerMovement: MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    
    private MovementModule _module;
    
    private float _horizontalInput;
    private float _verticalInput;
    private bool _isSprintInput;
    
    public void Construct(MovementModule module)
    {
        _module = module;
    }

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
        _isSprintInput = Input.GetKey(KeyCode.LeftShift);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var input = new Vector2(_horizontalInput, _verticalInput).normalized;

        var sprintMultiplier = _isSprintInput
            ? _module.SprintMultiplier
            : 1f;

        var maxSpeed = _module.MoveSpeed * sprintMultiplier;
        var targetVelocity = input * maxSpeed;

        var hasInput = input.sqrMagnitude > 0f;

        var time = hasInput
            ? _module.Acceleration
            : _module.Inertia;

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