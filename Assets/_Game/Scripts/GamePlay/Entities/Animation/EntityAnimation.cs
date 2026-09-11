using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Entities.Animation
{
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class EntityAnimation : MonoBehaviour
{
    private SpriteRenderer _renderer;
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
        _collider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        _collider.isTrigger = !_config.IsObstacle;
        
        SetSprite(_config.Sprite);
    }

    private void SetSprite(Sprite sprite) => _renderer.sprite = sprite;
}
}