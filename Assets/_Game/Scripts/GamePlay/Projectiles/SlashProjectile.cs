using System.Collections;
using _Game.Scripts.GamePlay.Interfaces;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Projectiles
{
public class SlashProjectile: Projectile
{
    [SerializeField] private float _hitTime = 0.1f;
    [SerializeField] private bool _followSource;
    
    public override void SetHit(HitInfo hit)
    {
        base.SetHit(hit);
        if (!_followSource)
        {
            transform.SetParent(null);
        }
        StartCoroutine(Hit());
    }

    private IEnumerator Hit()
    {
        yield return new WaitForSeconds(_hitTime);
        Destroy(gameObject);
    }
}
}