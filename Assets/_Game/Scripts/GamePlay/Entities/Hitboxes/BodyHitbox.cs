using System;
using _Game.Scripts.GamePlay.Interfaces;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Hitboxes
{
public class BodyHitbox : MonoBehaviour, IDamageReceiver
{
    public event Action<HitInfo> OnHit;
    
    public event Action<IDamageReceiver> OnDamageReceiver;

    private void OnTriggerEnter2D(Collider2D other) => TryGetEntity(other);

    private void OnCollisionEnter2D(Collision2D other) => TryGetEntity(other.collider);

    private void TryGetEntity(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageReceiver receiver))
        {
            OnDamageReceiver?.Invoke(receiver);
        }
    }

    public void TakeDamage(HitInfo hit)
    {
        OnHit?.Invoke(hit);
    }
}
}