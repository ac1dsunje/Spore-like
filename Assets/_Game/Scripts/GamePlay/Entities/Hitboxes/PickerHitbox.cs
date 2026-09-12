using System;
using System.Collections.Generic;
using _Game.Scripts.GamePlay.Drops;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Hitboxes
{
[RequireComponent(typeof(CircleCollider2D))]
public class PickerHitbox : MonoBehaviour
{
    public event Action<Drop> OnPicked;
    
    private CircleCollider2D _collider;
    private readonly List<Drop> _drops = new();

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
    }
    
    public void SetSize(float size)
    {
        _collider.enabled = size > 0.1f;
        _collider.radius = size;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Drop drop)) return;
        
        if (_drops.Contains(drop)) return;

        _drops.Add(drop);
        OnPicked?.Invoke(drop);
    }

    public void DestroyDrop(Drop drop)
    {
        _drops.Remove(drop);
        Destroy(drop.gameObject);
    }
}
}