using _Game.Scripts.Player;
using UnityEngine;

namespace _Game.Scripts.World.Biome.Chunk.Environment.Enemies
{
public class Spike: MonoBehaviour, IDamageAble
{
    [SerializeField] private float _damage = 3f;
    [SerializeField] private float _maxHealth = 40f;

    private float _health;

    private void Awake()
    {
        _health = _maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        TryAttack(other);
    }

    private void TryAttack(Collision2D other)
    {
        if (!other.collider.TryGetComponent(out IDamageAble damageAble)) return;
        damageAble.TakeDamage(_damage, this);
    }

    public void TakeDamage(float amount, IDamageAble damager)
    {
        _health -= amount;
        _health = Mathf.Max(0f, _health);
        if (_health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
}