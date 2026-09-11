using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Interfaces;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Projectiles
{
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PolygonCollider2D))]
public class Projectile : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private PolygonCollider2D _collider;
    
    private ProjectileConfig _config;
    
    private HitInfo _setHit;
    private HitInfo _realHit;

    private int _hitsDone;
    
    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        transform.Translate(transform.right * (_config.Speed * Time.deltaTime), Space.World);
    }

    private void SetColliderShape(Sprite sprite)
    {
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
    
    public void Initialize(ProjectileConfig config, Transform source, HitInfo hitInfo)
    {
        _config = config;
        if (_config.FollowSource)
        {
            transform.SetParent(source);
        }
        SetSprite(_config.Sprite);
        SetColliderShape(_config.Sprite);
        
        SetHit(hitInfo);
    }
    
    private void SetHit(HitInfo hit)
    {
        _setHit = hit;
        
        _realHit = new(
            _setHit.Damage, 
            _setHit.IgnoreResistance, 
            _setHit.Source,
            _config.Type == ProjectileType.Melee ? _setHit.Receiver : null);
        
        _setHit.AddDamage(_config.AdditionalDamage);
        _setHit.AddIgnoreResistance(_config.IgnoreResistance);
        StartCoroutine(Hit());
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            OnTrigger(damageReceiver);
        }
    }

    private void OnTrigger(IDamageReceiver damageReceiver)
    {
        if (damageReceiver == _setHit.Receiver) return;
        
        damageReceiver.TakeDamage(_realHit);
        _hitsDone++;
        if (_config.MaxHits > 0 && _hitsDone >= _config.MaxHits)
        {
            Destroy(gameObject);
        }
    }
    
    private void SetSprite(Sprite sprite) => _renderer.sprite = sprite;

    private IEnumerator Hit()
    {
        yield return new WaitForSeconds(_config.HitTime);
        Destroy(gameObject);
    }
}
}