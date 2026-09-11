using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Entities.Animation
{
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class EntityAnimation : MonoBehaviour, IVisible, ISocial
{
    private SpriteRenderer _renderer;
    private CircleCollider2D _collider;
    
    private AnimationSettings _config;
    private SocialModule _social;
    private Transform _transform;

    [Inject]
    private void Construct(AnimationSettings config, Transform entityTransform, SocialModule socialModule)
    {
        _config = config;
        _social = socialModule;
        _transform = entityTransform;
    }

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        _collider.isTrigger = !_config.IsObstacle;
        
        SetSprite(_config.Sprite);
    }

    public Transform GetTransform() => _transform;
    public float GetInfluence() => _social.Influence;

    private void SetSprite(Sprite sprite) => _renderer.sprite = sprite;
}
}