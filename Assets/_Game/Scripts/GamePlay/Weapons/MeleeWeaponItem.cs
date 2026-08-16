using System.Collections;
using _Game.Scripts.GamePlay.Interfaces;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Weapons
{
public class MeleeWeaponItem: MonoBehaviour
{
    [SerializeField] private float _hitTime = 0.1f;
    
    private HitInfo _hit;
    
    public void SetHit(HitInfo hit)
    {
        _hit = hit;
        StartCoroutine(Hit());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageAble damageAble))
        {
            if (damageAble == _hit.Owner) return;
            _hit.Owner.SetDamageDealt(damageAble.TakeDamage(_hit));
        }
    }

    private IEnumerator Hit()
    {
        yield return new WaitForSeconds(_hitTime);
        gameObject.SetActive(false);
    }
}
}