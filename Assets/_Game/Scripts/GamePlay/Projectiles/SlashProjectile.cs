using System.Collections;
using _Game.Scripts.GamePlay.Interfaces;
using _Game.Scripts.GamePlay.Weapons;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Projectiles
{
public class SlashProjectile: Projectile
{
    [SerializeField] private float _hitTime = 0.1f;
    
    public override void SetHit(HitInfo hit)
    {
        base.SetHit(hit);
        StartCoroutine(Hit());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            if (damageReceiver == HitInfo.Receiver) return;
            damageReceiver.TakeDamage(HitInfo);
        }
    }

    private IEnumerator Hit()
    {
        yield return new WaitForSeconds(_hitTime);
        Destroy(gameObject);
    }
}
}