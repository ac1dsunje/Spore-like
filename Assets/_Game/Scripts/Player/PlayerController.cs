using _Game.Scripts.Food;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace _Game.Scripts.Player
{
public enum PlayerState
{
    Moving, 
    Disabled
}
public class PlayerController: MonoBehaviour, IEater
{
    [SerializeField] private Light2D _light;
    public PlayerStats Stats { get; private set; }
    
    private Rigidbody2D _rigidbody;
    private PlayerState _state;

    private float _horizontalVelocity;
    private float _verticalVelocity;

    public void Eat(int amount)
    {
        Stats.AddExperience(amount);
    }

    public void Disable() => SetState(PlayerState.Disabled);

    public void Enable() => SetState(PlayerState.Moving);
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Construct(PlayerStats stats)
    {
        Stats = stats;
    }

    private void Update()
    {
        ReadInput();
        _light.pointLightOuterRadius = Stats.VisionRadius;
    }

    private void ReadInput()
    {
        _horizontalVelocity = IsInState(PlayerState.Disabled)? 0: Input.GetAxis("Horizontal");
        _verticalVelocity = IsInState(PlayerState.Disabled)? 0: Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rigidbody.linearVelocity = new Vector2(_horizontalVelocity * Stats.MoveSpeed, _verticalVelocity * Stats.MoveSpeed);
    }
    
    private void SetState(PlayerState state) => _state = state;
    private bool IsInState(PlayerState state) => _state == state;
}
}