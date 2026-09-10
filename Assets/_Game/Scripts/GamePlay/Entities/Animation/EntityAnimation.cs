using _Game.Scripts.GamePlay.Interfaces;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Entities.Animation
{
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CircleCollider2D))]
public class EntityAnimation: MonoBehaviour, IVisible, ISocial
{
    private SpriteRenderer _renderer;
    private Animator _animator;
    private CircleCollider2D _collider;
    
    private AnimationSettings _config;
    private EntityModel _model;

    [Inject]
    private void Construct(AnimationSettings config, EntityModel model)
    {
        _config = config;
        _model = model;
    }

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        _collider.isTrigger = !_config.IsObstacle;
        
        SetSprite(_config.Sprite);
        SetAnimator(_config.Controller);
    }

    public Transform GetTransform() => _model.Movement.Transform;
    public float GetInfluence() => _model.Social.Influence;

    private void SetSprite(Sprite sprite) => _renderer.sprite = sprite;
    
    private void SetAnimator(RuntimeAnimatorController controller) => _animator.runtimeAnimatorController = controller;
}
}