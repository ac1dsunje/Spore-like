using System;
using _Game.Scripts.GamePlay.Interfaces;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Entities.Hitboxes
{
public class EntityBodyHitbox : MonoBehaviour, IDamageReceiver
{
    public event Action<HitInfo> OnHit;

    public void TakeDamage(HitInfo hit)
    {
        OnHit?.Invoke(hit);
    }
}
}