using _Game.Scripts.Player;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace _Game.Scripts.World.Biome.Chunk.Environment.Obstacles
{
public class Spike: MonoBehaviour
{
    [SerializeField] private float _damage;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.collider.TryGetComponent(out IDamageAble damageAble)) return;
        damageAble?.TakeDamage(_damage);
    }
}
}