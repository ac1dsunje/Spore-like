using _Game.Scripts.GamePlay.Player.Modules;
using UnityEngine;

namespace _Game.Scripts.GamePlay
{
public class AttackItem: MonoBehaviour
{
    private float _damage = 10;
    
    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageAble damageAble))
        {
            damageAble.TakeDamage(_damage, null);
        }
    }
}
}