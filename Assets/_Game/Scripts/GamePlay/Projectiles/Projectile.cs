using _Game.Scripts.GamePlay.Interfaces;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Projectiles
{
public abstract class Projectile: MonoBehaviour
{
    [SerializeField] protected ProjectileConfig Config;
    private HitInfo _hitInfo;
    
    public void SetSource(Transform source)
    {
        if (Config.FollowSource)
        {
            transform.SetParent(source);
        }
    }
    
    public virtual void SetHit(HitInfo hit)
    {
        _hitInfo = hit;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            OnTrigger(damageReceiver);
        }
    }

    protected virtual void OnTrigger(IDamageReceiver damageReceiver)
    {
        if (damageReceiver == _hitInfo.Receiver) return;
        damageReceiver.TakeDamage(_hitInfo);
    }
}
}