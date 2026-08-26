using UnityEngine;

namespace _Game.Scripts.GamePlay.Movement
{
[RequireComponent(typeof(Rigidbody2D))]
public class MovementController: MonoBehaviour
{
    public Vector3Int GridPosition => new(
        Mathf.RoundToInt(_rigidbody.position.x),
        Mathf.RoundToInt(_rigidbody.position.y),
        0
    );
    
    private PhysicsMaterial2D _material2D;

    public void SetMaterial(float friction, float bounciness)
    {
        var material2D = new PhysicsMaterial2D
        {
            friction = friction,
            bounciness = bounciness
        };

        if (!_rigidbody)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        if (_material2D != material2D)
        {
            _rigidbody.linearDamping = friction;
            _rigidbody.sharedMaterial = material2D;
        
            _material2D = material2D;
        }
    }
    
    public bool IsMoving => _rigidbody.linearVelocity != Vector2.zero;
    
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Push(Vector2 direction, float power)
    {
        _rigidbody.AddForce(direction * power, ForceMode2D.Impulse);
    }
    
    public void Move(Vector2 targetVelocity, float rate)
    {
        _rigidbody.linearVelocity = Vector2.MoveTowards(_rigidbody.linearVelocity, targetVelocity, rate);
    }
    
    public void Flip(bool facingRight)
    {
        _rigidbody.transform.localScale = new Vector3(!facingRight ? -1 : 1, 1, 1);
    }
}
}