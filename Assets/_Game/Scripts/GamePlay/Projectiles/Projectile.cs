using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Interfaces;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Projectiles
{
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PolygonCollider2D))]
public class Projectile: MonoBehaviour
{
    private ProjectileConfig _config;
    
    private SpriteRenderer _renderer;
    private Animator _animator;
    private PolygonCollider2D _collider;
    
    private Sprite _currentSprite;
    
    private HitInfo _hitInfo;
    
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
    
    public void Initialize(ProjectileConfig config, Transform source)
    {
        _config = config;
        if (_config.FollowSource)
        {
            transform.SetParent(source);
        }
        SetSprite(_config.Sprite);
        SetAnimator(_config.Controller);
    }
    
    public void SetHit(HitInfo hit)
    {
        _hitInfo = hit;
        StartCoroutine(Hit());
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            OnTrigger(damageReceiver);
        }
    }

    protected void OnTrigger(IDamageReceiver damageReceiver)
    {
        if (damageReceiver == _hitInfo.Receiver) return;
        damageReceiver.TakeDamage(_hitInfo);
    }
    
    private void SetSprite(Sprite sprite) => _renderer.sprite = sprite;
    
    private void SetAnimator(RuntimeAnimatorController controller) => _animator.runtimeAnimatorController = controller;

    private IEnumerator Hit()
    {
        yield return new WaitForSeconds(_config.HitTime);
        Destroy(gameObject);
    }
}
}