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
    
    private Sprite _currentSprite;
    
    [Inject] private AnimationSettings _config;
    [Inject] private EntityModel _model;

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

    public EntityModel GetEntityModel() => _model;
    public float GetInfluence() => _model.Social.Influence;

    private void SetSprite(Sprite sprite) => _renderer.sprite = sprite;
    
    private void SetAnimator(RuntimeAnimatorController controller) => _animator.runtimeAnimatorController = controller;
}
}