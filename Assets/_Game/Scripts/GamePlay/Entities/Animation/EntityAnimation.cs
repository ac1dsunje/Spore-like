using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Entities.Animation
{
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CircleCollider2D))]
public class EntityAnimation : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Animator _animator;
    private CircleCollider2D _collider;
    
    private AnimationSettings _config;

    [Inject]
    private void Construct(AnimationSettings config)
    {
        _config = config;
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

    private void SetSprite(Sprite sprite) => _renderer.sprite = sprite;
    
    private void SetAnimator(RuntimeAnimatorController controller) => _animator.runtimeAnimatorController = controller;
}
}