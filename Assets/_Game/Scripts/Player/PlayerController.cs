using System;
using _Game.Scripts.Food;
using UnityEngine;

namespace _Game.Scripts.Player
{
public class PlayerController: MonoBehaviour, IEater
{
    [SerializeField] private PlayerConfig _config;
    
    [SerializeField] private float _levelSet = 3;
    [SerializeField] private float _levelScaler = 1.5f;
    
    private float _experience;
    private int _level;
    
    public event Action<float> OnExperienceChanged;
    public event Action<int> OnLevelChanged;
    

    private Rigidbody2D _rigidbody;

    private float _horizontalVelocity;
    private float _verticalVelocity;

    public void Eat(float amount)
    {
        _experience += amount;
        OnExperienceChanged?.Invoke(_experience);
        
        if (_experience < _levelSet) return;
        
        UpdateLevel();
    }

    private void UpdateLevel()
    {
        _experience -= _levelSet;
        OnExperienceChanged?.Invoke(_experience);
        _level++;
        OnLevelChanged?.Invoke(_level);
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