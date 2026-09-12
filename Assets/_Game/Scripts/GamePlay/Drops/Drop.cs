using System;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Drops
{
[RequireComponent(typeof(SpriteRenderer))]
public class Drop : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private DropConfig _config;
        
    private Transform _target;
    private float _flySpeed;
    
    public event Action<Drop> OnReachedTarget;

    public DropType Type => _config.DropType;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetConfig(DropConfig config)
    {
        _config = config;
        _spriteRenderer.sprite = config.Sprite;
    }
    
    public void FlyTo(Transform target, float speed)
    {
        _target = target;
        _flySpeed = speed;
        
        if (TryGetComponent(out Collider2D collider))
        {
            collider.enabled = false;
        }
    }

    private void Update()
    {
        if (_target == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position, 
            _target.position, 
            _flySpeed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, _target.position) < 0.05f)
        {
            OnReachedTarget?.Invoke(this);
        }
    }
}
}