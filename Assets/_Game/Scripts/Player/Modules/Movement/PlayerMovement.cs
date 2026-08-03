using UnityEngine;

namespace _Game.Scripts.Player.Modules.Movement
{

public class PlayerMovement: MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    
    private MovementModule _module;
    
    private float _horizontalInput;
    private float _verticalInput;
    
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
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var targetVelocity = new Vector2(_horizontalInput, _verticalInput).normalized * _module.MoveSpeed;

        var time = (Mathf.Abs(_horizontalInput) > 0.1f || Mathf.Abs(_verticalInput) > 0.1f)
            ? _module.Acceleration
            : _module.Inertia;

        var rate = _module.MoveSpeed / time;

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