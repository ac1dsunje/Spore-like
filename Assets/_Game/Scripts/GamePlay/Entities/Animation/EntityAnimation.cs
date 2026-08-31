using System.Collections.Generic;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Modules;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.GamePlay.Entities.Animation
{
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PolygonCollider2D))]
public class EntityAnimation: MonoBehaviour, IVisible
{
    private SpriteRenderer _renderer;
    private Animator _animator;
    private PolygonCollider2D _collider;
    
    private Sprite _currentSprite;
    
    [Inject] private DisguiseModule _disguise;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
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
    
    public bool IsDetected(float sensorics)
    {
        return _disguise.TryNotice(sensorics);
    }
    
    public void SetVisible(bool visible)
    {
        _renderer.enabled = visible;
        _animator.enabled = visible;
    }

    public void SetConfig(AnimationSettings settings)
    {
        SetSprite(settings.Sprite);
        SetAnimator(settings.Controller);
    }

    private void SetSprite(Sprite sprite) => _renderer.sprite = sprite;
    
    private void SetAnimator(RuntimeAnimatorController controller) => _animator.runtimeAnimatorController = controller;
}
}