using System.Collections;
using _Game.Scripts.GamePlay.Player.Modules;
using UnityEngine;

namespace _Game.Scripts.GamePlay
{
public class AttackItem: MonoBehaviour
{
    private float _damage;
    
    public void SetDamage(float damage)
    {
        _damage = damage;
        StartCoroutine(Hit());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageAble damageAble) && _damage > 0)
        {
            damageAble.TakeDamage(_damage, null);
        }
    }

    private IEnumerator Hit()
    {
        yield return new WaitForSeconds(0.2f);
        _damage = 0;
        gameObject.SetActive(false);
    }
}
}