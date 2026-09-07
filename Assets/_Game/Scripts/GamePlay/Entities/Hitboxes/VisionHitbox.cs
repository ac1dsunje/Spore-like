using System;
using _Game.Scripts.GamePlay.Interfaces;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Hitboxes
{
[RequireComponent(typeof(BoxCollider2D))]
public class VisionHitbox: MonoBehaviour
{
    private BoxCollider2D _collider;
    public event Action<IVisible> OnEntityEntered;
    public event Action<IVisible> OnEntityLeft;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }
    
    public void SetSize(Vector2 size)
    {
        _collider.size = size;
        if (size.y > 0.1f)
            _collider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<IVisible>(out var visible)) return;
        OnEntityEntered?.Invoke(visible);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent<IVisible>(out var visible)) return;
        OnEntityLeft?.Invoke(visible);
    }
}
}