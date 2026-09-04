using System;
using _Game.Scripts.GamePlay.Entities.Drops;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Hitboxes
{
[RequireComponent(typeof(CircleCollider2D))]
public class PickerHitbox: MonoBehaviour
{
    public event Action<DropType> OnPicked;
    
    private CircleCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
    }
    
    public void SetSize(float size)
    {
        _collider.radius = size;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Drop drop)) return;
        OnPicked?.Invoke(drop.GetDropType());
        Destroy(drop.gameObject);
    }
}
}