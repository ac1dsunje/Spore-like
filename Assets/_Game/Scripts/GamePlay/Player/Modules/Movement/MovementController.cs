using UnityEngine;

namespace _Game.Scripts.GamePlay.Player.Modules.Movement
{
[RequireComponent(typeof(Rigidbody2D))]
public class MovementController: MonoBehaviour
{
    
    public Vector3Int GridPosition => new(
        Mathf.RoundToInt(_rigidbody.position.x),
        Mathf.RoundToInt(_rigidbody.position.y),
        0
    );
    
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Push(Vector2 direction)
    {
        _rigidbody.AddForce(direction, ForceMode2D.Impulse);
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