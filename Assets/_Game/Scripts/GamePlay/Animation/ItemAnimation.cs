using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace _Game.Scripts.GamePlay.Animation
{
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ShadowCaster2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class ItemAnimation: MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Animator _animator;
    private ShadowCaster2D _shadowCaster;
    private PolygonCollider2D _collider;
    
    private Sprite _currentSprite;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _shadowCaster = GetComponent<ShadowCaster2D>();
        _collider = GetComponent<PolygonCollider2D>();
    }

    private void LateUpdate()
    {
        SetColliderShape(_renderer.sprite);
    }

    private void SetColliderShape(Sprite sprite)
    {
        _collider.enabled = true;

        if (_currentSprite == sprite) return;
        _currentSprite = sprite;

        var shapeCount = sprite.GetPhysicsShapeCount();
        _collider.pathCount = shapeCount;

        var shape = new List<Vector2>();

        for (var i = 0; i < shapeCount; i++)
        {
            shape.Clear();

            sprite.GetPhysicsShape(i, shape);
            _collider.SetPath(i, shape);
        }
    }
    
    public void SetVisible(bool visible)
    {
        _renderer.enabled = visible;
        _animator.enabled = visible;
    }

    public void SetConfig(AnimationSettings settings)
    {
        _shadowCaster.enabled = settings.CastShadows;
        _renderer.sprite = settings.Sprite;
        if (settings.Controller)
        {
            _animator.runtimeAnimatorController = settings.Controller;
        }
    }
}
}