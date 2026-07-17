using _Game.Scripts.Food;
using UnityEngine;

namespace _Game.Scripts.Player
{
public class PlayerController: MonoBehaviour, IEater
{
    [SerializeField] private PlayerConfig _config;
    // SerializeFields must be deleted after overlay added
    [SerializeField] private int _experience;
    [SerializeField] private int _level;
    [SerializeField] private int _levelSet = 2;
    [SerializeField] private int _levelScaler = 2;

    private Rigidbody2D _rigidbody;

    private float _horizontalVelocity;
    private float _verticalVelocity;

    public void Eat(int amount)
    {
        _experience += amount;
        
        if (_experience < _levelSet) return;
        
        UpdateLevel();
    }

    private void UpdateLevel()
    {
        _experience -= _levelSet;
        _level++;
        _levelSet *= _levelScaler;
    }
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Reset();
    }

    private void Reset()
    {
        _experience = 0;
        _level = 0;
    }

    private void Update()
    {
        GetVelocity();
    }

    private void GetVelocity()
    {
        _horizontalVelocity = Input.GetAxis("Horizontal") * _config.MoveSpeed;
        _verticalVelocity = Input.GetAxis("Vertical") * _config.MoveSpeed;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rigidbody.linearVelocity = new Vector2(_horizontalVelocity, _verticalVelocity);
    }
}
}