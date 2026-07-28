using _Game.Scripts.Player;
using UnityEngine;

namespace _Game.Scripts.World.Biome.Chunk.Environment.Enemies
{
public class Spike: MonoBehaviour, IDamageAble
{
    [SerializeField] private float _damage = 3f;
    [SerializeField] private float _health = 10f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        TryAttack(other);
    }

    private void TryAttack(Collision2D other)
    {
        if (!other.collider.TryGetComponent(out IDamageAble damageAble)) return;
        TakeDamage(damageAble.TakeDamage(_damage));
    }

    public float TakeDamage(float amount)
    {
        _health -= amount;
        _health = Mathf.Max(0f, _health);
        if (_health <= 0f)
        {
            Destroy(gameObject);
        }
        return 0;
    }
}
}