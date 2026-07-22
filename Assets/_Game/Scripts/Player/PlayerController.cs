using _Game.Scripts.World.Food;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace _Game.Scripts.Player
{
public enum PlayerState
{
    Moving, 
    Disabled
}
public class PlayerController: MonoBehaviour, IEater, IDamageAble
{
    [SerializeField] private Light2D _light;
    [SerializeField] private CircleCollider2D _visionCollider;
    public PlayerStats Stats { get; private set; }
    
    private Rigidbody2D _rigidbody;
    private PlayerState _state;

    private float _horizontalVelocity;
    private float _verticalVelocity;

    public void Eat(int amount, FoodItem food)
    {
        Stats.Eat(amount, food);
    }

    public float TakeDamage(float amount)
    {
        return Stats.TakeDamage(amount);
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
        _visionCollider.radius = Stats.VisionRadius;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent<FoodItem>(out var food);
        if (food == null) return;
        Stats.DiscoverFood(food);
    }

    private void SetState(PlayerState state) => _state = state;
    private bool IsInState(PlayerState state) => _state == state;
}
}