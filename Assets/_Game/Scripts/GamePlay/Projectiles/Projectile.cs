using _Game.Scripts.GamePlay.Interfaces;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Projectiles
{
public abstract class Projectile: MonoBehaviour
{
    private HitInfo _hitInfo;
    
    public virtual void SetHit(HitInfo hit)
    {
        _hitInfo = hit;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            DoDamage(damageReceiver);
        }
    }

    protected virtual void DoDamage(IDamageReceiver damageReceiver)
    {
        if (damageReceiver == _hitInfo.Receiver) return;
        damageReceiver.TakeDamage(_hitInfo);
    }
}
}