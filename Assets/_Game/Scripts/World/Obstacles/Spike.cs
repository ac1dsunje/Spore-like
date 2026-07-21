using _Game.Scripts.Player;
using UnityEngine;

namespace _Game.Scripts.World.Obstacles
{
public class Spike: MonoBehaviour
{
    [SerializeField] private float _damage;

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.collider.TryGetComponent(out IDamageAble damageAble);
        damageAble?.TakeDamage(_damage);
    }
}
}