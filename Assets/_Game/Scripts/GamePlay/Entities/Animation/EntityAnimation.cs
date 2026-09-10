using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
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
    private EntityScope _entity;

    [Inject]
    private void Construct(AnimationSettings config, EntityScope model)
    {
        _config = config;
        _entity = model;
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

    public Transform GetTransform() => _entity.Get<MovementModule>().Transform;
    public float GetInfluence() => _entity.Get<SocialModule>().Influence;

    private void SetSprite(Sprite sprite) => _renderer.sprite = sprite;
    
    private void SetAnimator(RuntimeAnimatorController controller) => _animator.runtimeAnimatorController = controller;
}
}