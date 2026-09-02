using System;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.World.Food;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Hitboxes
{
public class BodyHitbox : MonoBehaviour, IDamageReceiver, IBiteable
{
    public event Action<HitInfo> OnHit;
    public event Action<float, float> OnBite;
    public event Action<int> OnEaten;
    
    public event Action<IDamageReceiver> OnDamageReceiver;
    public event Action<IBiteable> OnBiteAble;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageReceiver receiver))
        {
            OnDamageReceiver?.Invoke(receiver);
        }

        if (other.TryGetComponent(out IBiteable biteable))
        {
            OnBiteAble?.Invoke(biteable);
        }
    }

    public void TakeDamage(HitInfo hit)
    {
        OnHit?.Invoke(hit);
    }

    public void TakeBite(float damage, float penetration)
    {
        OnBite?.Invoke(damage, penetration);
    }
    
    public void SetEaten(int eaten)
    {
        OnEaten?.Invoke(eaten);
    }
}
}