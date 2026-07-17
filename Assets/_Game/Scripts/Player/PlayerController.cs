using _Game.Scripts.Food;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Scripts.Player
{
public class PlayerController: MonoBehaviour, IEater
{
    [SerializeField] private PlayerConfig _config;

    private Rigidbody2D _rigidbody;

    private float _horizontalVelocity;
    private float _verticalVelocity;

    public bool Eat(int amount)
    {
        Debug.Log($"Eater amount: {amount}");
        return true;
    }
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _horizontalVelocity = Input.GetAxis("Horizontal") * _config.MoveSpeed;
        _verticalVelocity = Input.GetAxis("Vertical") * _config.MoveSpeed;
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector2(_horizontalVelocity, _verticalVelocity);
    }
}
}