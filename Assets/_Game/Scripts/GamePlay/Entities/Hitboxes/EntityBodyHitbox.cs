using System;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.World.Food;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Hitboxes
{
public class EntityBodyHitbox : MonoBehaviour, IDamageReceiver, IBiteable
{
    public event Action<HitInfo> OnHit;
    public event Action<float, float> OnBite;
    public event Action<int> OnEaten;

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