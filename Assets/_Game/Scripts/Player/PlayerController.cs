using UnityEngine;

namespace _Game.Scripts.Player
{
public class PlayerController: MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _rigidbody;

    private float _horizontalVelocity;
    private float _verticalVelocity;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _horizontalVelocity = Input.GetAxis("Horizontal") * _moveSpeed;
        _verticalVelocity = Input.GetAxis("Vertical") * _moveSpeed;
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector2(_horizontalVelocity, _verticalVelocity);
    }
}
}