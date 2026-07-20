using System;
using _Game.Scripts.Food;
using UnityEngine;

namespace _Game.Scripts.Player
{
public enum PlayerState
{
    Moving, 
    Disabled
}
public class PlayerController: MonoBehaviour, IEater
{
    [SerializeField] private PlayerConfig _config;
    
    private float _experience;
    private float _levelSet;
    private int _level;
    
    public event Action<float> OnExperienceChanged;
    public event Action<int> OnLevelChanged;
    
    private Rigidbody2D _rigidbody;
    private PlayerState _state;

    private float _horizontalVelocity;
    private float _verticalVelocity;

    public void Eat(float amount)
    {
        UpdateExperience(amount);
        UpdateLevel();
    }

    public void Disable() => SetState(PlayerState.Disabled);

    public void Enable() => SetState(PlayerState.Moving);

    private void UpdateLevel()
    {
        while (_experience >= _levelSet)
        {
            UpdateExperience(-_levelSet);
            _level++;
            OnLevelChanged?.Invoke(_level);
            _levelSet *= _config.ExperienceConfig._levelScaler;
        }
    }

    private void UpdateExperience(float amount)
    {
        _experience += amount;
        OnExperienceChanged?.Invoke(_experience);
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
        _levelSet = _config.ExperienceConfig._levelSet;
    }

    private void Update()
    {
        if (_state == PlayerState.Disabled) return;
        ReadInput();
    }

    private void ReadInput()
    {
        _horizontalVelocity = Input.GetAxis("Horizontal");
        _verticalVelocity = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rigidbody.linearVelocity = new Vector2(_horizontalVelocity * _config.MovementConfig.MoveSpeed, _verticalVelocity * _config.MovementConfig.MoveSpeed);
    }
    
    private void SetState(PlayerState state) => _state = state;
}
}