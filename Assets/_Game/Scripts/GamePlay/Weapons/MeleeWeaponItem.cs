using System.Collections;
using _Game.Scripts.GamePlay.Player.Modules;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Weapons
{
public class MeleeWeaponItem: MonoBehaviour
{
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
            damageAble.TakeDamage(_hit);
        }
    }

    private IEnumerator Hit()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
}
}