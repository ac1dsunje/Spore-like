using System;
using _Game.Scripts.GamePlay.Interfaces;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Hitboxes
{
[RequireComponent(typeof(BoxCollider2D))]
public class VisionHitbox : MonoBehaviour
{
    private BoxCollider2D _collider;
    public event Action<IVisible> OnEntityEntered;
    public event Action<IVisible> OnEntityLeft;
    public event Action<ISocial> OnSocialEntityEntered;
    public event Action<ISocial> OnSocialEntityLeft;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }
    
    public void SetSize(Vector2 size)
    {
        _collider.size = size;
        _collider.enabled = size.y > 0.1f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IVisible>(out var visible)) OnEntityEntered?.Invoke(visible);
        if (other.TryGetComponent<ISocial>(out var social)) OnSocialEntityEntered?.Invoke(social);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<IVisible>(out var visible)) OnEntityLeft?.Invoke(visible);
        if (other.TryGetComponent<ISocial>(out var social)) OnSocialEntityLeft?.Invoke(social);
    }
}
}